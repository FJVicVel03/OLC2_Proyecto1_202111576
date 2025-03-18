grammar Language;

program: dcl*;

dcl: varDcl | stmt | funcDcl | classDcl;

varDcl: 'var' ID ':' type ('=' expr)? ';'   
		| 'var' ID '=' expr ';';

type: 'int' | 'float' | 'bool' | 'string' | 'rune';

funcDcl: 'function' ID '(' params? ')' '{' dcl* '}' ;

classDcl: 'class' ID '{' classBody* '}' ;

classBody: varDcl | funcDcl;

params: ID (',' ID)*;

stmt: 
expr ';' # ExprStmt
//| 'print(' expr ')' ';' # PrintStmt
|'{' dcl* '}' # BlockStmt
| 'if' '(' expr ')' stmt ('else' stmt)? # IfStmt
| 'while' '(' expr ')' stmt # WhileStmt
| 'for' '(' forInit expr ';' expr ')' stmt # ForStmt
| 'do' stmt 'while' '(' expr ')' ';' # DoWhileStmt
| 'break' ';' # BreakStmt
| 'continue' ';' # ContinueStmt
| 'return' expr? ';' # ReturnStmt;



forInit: varDcl | expr ';'; 

expr:
	 '-' expr						# Negate
	| expr call+ 				# Callee 
	| expr op = ('*' | '/') expr	# MulDiv
	| expr op = ('+' | '-') expr	# AddSub
	| expr op = ('>' | '<' | '>=' | '<=') expr	# Relational
	| expr op = ('==' | '!=') expr	# Equality
	| expr '=' expr						# Assign	
	| BOOL							# Boolean
	| FLOAT						# Float
	| STRING						# String
	| INT							# Int
	| RUNE							# Rune
	| 'new' ID '(' args? ')'		# New
	| ID							# Identifier
	| '(' expr ')'			# Parens;
	
call: '(' args? ')' # FuncCall | '.' ID # Get; 
args: expr (',' expr)*;	

INT: [0-9]+;
BOOL: 'true' | 'false';
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' ~["]* '"';
RUNE: '\'' (~['\\] | '\\' .) '\''; 
WS: [ \t\r\n]+ -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
COMMENT: '//' ~[\r\n]* -> skip;
ML_COMMENT: '/*' .*? '*/' -> skip;

