grammar ExcGrammar;
/* 
* Parser Rules 
*/ 
compileUnit : expression EOF; 
expression : 
'-' expression  #UnminExpr
|LPAREN expression RPAREN #ParenthesizedExpr 
|expression EXPONENT expression #ExponentialExpr 
 | expression operatorToken=(MULTIPLY | DIVIDE) expression #MultiplicativeExpr
 | expression operatorToken=(MOD | DIV) expression #ModDivExpression
| expression operatorToken=(ADD | SUBTRACT) expression #AdditiveExpr
| NUMBER #NumberExpr
| operatorToken=( MMIN | MMAX) LPAREN paramlist=arglist RPAREN #MinExpr;

arglist: expression (';' expression)+;

/* 
* Lexer Rules 
*/
MMIN : 'mmin';
MMAX : 'mmax';
MOD : 'mod';
DIV : 'div';
NUMBER : INT (',' INT)?;  
INT : ('0'..'9')+; 
EXPONENT : '^'; 
MULTIPLY : '*'; 
DIVIDE : '/'; 
SUBTRACT : '-'; 
ADD : '+'; 
LPAREN : '('; 
RPAREN : ')';
WS : [ \t\r\n] -> channel(HIDDEN); 
