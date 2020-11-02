
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace TestExcel
{
    class ExcGrammarVisitor : ExcGrammarBaseVisitor <double>
    {
        Dictionary<string, double> tableIdentifier = new Dictionary<string, double>();
        public override double VisitCompileUnit(ExcGrammarParser.CompileUnitContext context)
        {
            return Visit(context.expression());
        }
        public override double VisitNumberExpr(ExcGrammarParser.NumberExprContext context)
        {
            var result = double.Parse(context.GetText());
            Debug.WriteLine(result);
            return result;
        }

      

        //IdentifierExpr
        /*public override double VisitIdentifierExpr(ExcGrammarParser.IdentifierExprContext context)
        {
            var result = context.GetText();
            double value;
            //видобути значення змінної з таблиці
            if (tableIdentifier.TryGetValue(result.ToString(), out value))
            {
                return value;
            }
            else
            {
                return 0.0;
            }
        }*/

 

        public override double VisitParenthesizedExpr(ExcGrammarParser.ParenthesizedExprContext context)
        {
            return Visit(context.expression());
        }

        public override double VisitUnminExpr(ExcGrammarParser.UnminExprContext context)
        {
            var number = WalkLeft(context);
            Debug.WriteLine("-{0}", number);
            return -number;
        }

        public override double VisitExponentialExpr(ExcGrammarParser.ExponentialExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            Debug.WriteLine("{0} ^ {1}", left, right);
            return System.Math.Pow(left, right);
        }
        public override double VisitAdditiveExpr(ExcGrammarParser.AdditiveExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (context.operatorToken.Type == ExcGrammarLexer.ADD)
            {
                Debug.WriteLine("{0} + {1}", left, right);
                return left + right;
            }
            else //LabCalculatorLexer.SUBTRACT
            {
                Debug.WriteLine("{0} - {1}", left, right);
                return left - right;
            }
        }

        public override double VisitMinExpr(ExcGrammarParser.MinExprContext context)
        {

            if (context.operatorToken.Type == ExcGrammarLexer.MMIN)
            {
                double minValue = Double.PositiveInfinity;

                foreach (var child in context.paramlist.children.OfType<ExcGrammarParser.ExpressionContext>())
                {
                    double childValue = this.Visit(child);

                    if (childValue < minValue) minValue = childValue;
                }

                return minValue;
            }

            else 
            {
                double maxValue = Double.NegativeInfinity;

                foreach (var child in context.paramlist.children.OfType<ExcGrammarParser.ExpressionContext>())
                {
                    double childValue = this.Visit(child);

                    if (childValue > maxValue) maxValue = childValue;
                }

                return maxValue;
            }
        }

        public override double VisitModDivExpression(ExcGrammarParser.ModDivExpressionContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (right == 0) throw new Exception("Div by 0");
            if (context.operatorToken.Type == ExcGrammarLexer.MOD)
            {
                Debug.WriteLine("{0} mod {1}", left, right);
                return left % right;
            }

            else 
            {
                Debug.WriteLine("{0} div {1}", left, right);
                return (int)(left / right); 
            }
        }

        public override double VisitMultiplicativeExpr(ExcGrammarParser.MultiplicativeExprContext context)
        {
            var left = WalkLeft(context);
            var right = WalkRight(context);
            if (context.operatorToken.Type == ExcGrammarLexer.MULTIPLY)
            {
                Debug.WriteLine("{0} * {1}", left, right);
                return left * right;
            }

            else 
            {
                if (right == 0) throw new Exception("Div by 0");
                Debug.WriteLine("{0} / {1}", left, right);
                return left / right;
            }

}
private double WalkLeft(ExcGrammarParser.ExpressionContext context)
{
    return Visit(context.GetRuleContext<ExcGrammarParser.ExpressionContext>(0));
}
private double WalkRight(ExcGrammarParser.ExpressionContext context)
{
    return Visit(context.GetRuleContext<ExcGrammarParser.ExpressionContext>(1));
}
}
}


