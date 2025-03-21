grammar Language;

program: dcl*;

dcl: varDcl | stmt | funcDcl | classDcl |expr;

varDcl: 'var' ID ':' type ('=' expr)?    
		| 'var' ID '=' expr
		| ID ':=' expr;


type: 'int' | 'float' | 'bool' | 'string' | 'rune';

funcDcl: 'func' ID '(' params? ')' '{' dcl* '}' ;

classDcl: 'class' ID '{' classBody* '}' ;

classBody: varDcl | funcDcl;

params: ID (',' ID)*
		| 'var' ID ':' type (',' 'var' ID':' type)*;

stmt: 
expr ';' # ExprStmt
| 'fmt.Println(' args? ')' # PrintStmt
| '{' dcl* '}' # BlockStmt
| 'if'  expr  stmt ('else if' expr stmt)* ('else' stmt)? # IfStmt
| 'while' '(' expr ')' stmt # WhileStmt
| 'for' expr '{' stmt* '}' # ForConditionStmt // Variante condicional
| 'for'  forInit expr ';' expr  stmt # ForStmt
| 'do' stmt 'while' '(' expr ')'  # DoWhileStmt
| 'switch'  expr  '{' switchCase* '}' # SwitchStmt
| 'case' expr  stmt # CaseStatement
| 'default'  stmt # DefaultCaseStmt
| 'break'  # BreakStmt
| 'continue'  # ContinueStmt
| 'return' expr?  # ReturnStmt;

switchCase: caseStmt | defaultStmt;

caseStmt: 'case' expr ':' stmt;

defaultStmt: 'default' ':' stmt;

forInit: varDcl | expr ';'; 

expr:
	 '-' expr						# Negate
	|'!' expr						# Not
	| expr call+ 				# Callee 
	| expr op = ('*' | '/' | '%') expr	# MulDiv
	| expr op = ('+' | '-') expr	# AddSub
	| expr op = ('>' | '<' | '>=' | '<=') expr	# Relational
	| expr op = ('==' | '!=') expr	# Equality
	| expr op = ('+=' | '-=') expr	# AssignOp
	| expr op = ('&&' | '||') expr	# Logical
	| expr ('='| ':=') expr			# Assign
	| expr ('++' | '--')			# IncDec
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

