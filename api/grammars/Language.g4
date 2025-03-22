grammar Language;

program: dcl*;

dcl: varDcl | stmt | funcDcl | structDcl | structFuncDcl | expr;

varDcl: 'var' ID ':' type ('=' expr)?    
		| 'var' ID '=' expr
		| ID ':=' expr;

type: 'int' | 'float' | 'bool' | 'string' | 'rune' | ID;

funcDcl: 'func' ID '(' params? ')' '{' dcl* '}' ;

structDcl: 'type' ID 'struct' '{' structBody* '}' ;

structBody: ID ':' type ;

structFuncDcl: 'func' '(' ID ID ')' ID '(' params? ')' type? stmt;

params: ID (',' ID)*
		| 'var' ID ':' type (',' 'var' ID':' type)*;

stmt: 
expr  # ExprStmt
| 'fmt.Println(' args? ')' # PrintStmt
| '{' dcl* '}' # BlockStmt
| 'if'  expr  stmt ('else if' expr stmt)* ('else' stmt)? # IfStmt
| 'while' '(' expr ')' stmt # WhileStmt
| 'for' (expr)? '{' stmt* '}' # ForConditionStmt // Variante condicional
| 'for' ID ',' ID ':=' 'range' expr '{' stmt* '}' # ForRangeStmt
| 'for'  forInit expr ';' expr  stmt # ForStmt
| 'do' stmt 'while' '(' expr ')'  # DoWhileStmt
| 'switch'  expr  '{' switchCase* '}' # SwitchStmt
| 'break'  # BreakStmt
| 'continue'  # ContinueStmt
| 'return' expr?  # ReturnStmt;

switchCase: caseStmt | defaultStmt;

caseStmt: 'case' expr ':' stmt*;

defaultStmt: 'default' ':' stmt*;

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
	| '[]' type '{' expr (',' expr)* '}' #SliceInit
	| '[][]' type '{' matrixRow (',' matrixRow)* '}' # MultiDimSliceInit
	| expr '[' expr ']' '[' expr ']' # MultiDimSliceAccess
	| expr '[' expr ']' #SliceAccess
	| expr '[' expr ':' expr ']' #SliceRange
	| 'slices.Index' '(' expr ',' expr ')' #SliceIndexCall
	| 'strings.Join' '(' expr ',' expr ')' #StringsJoinCall
	| 'len' '(' expr ')' #LenCall
	| 'append' '(' expr ',' expr (',' expr)* ')' #AppendCall
	| 'strconv.Atoi' '(' expr ')' #AtoiCall
	| 'strconv.ParseFloat' '(' expr ')' #ParseFloatCall
	| 'reflect.TypeOf' '(' expr ')' #TypeOfCall
	| ID '{' structField (',' structField)* '}' #StructInit
	| expr '.' ID # StructAccess
	| BOOL							# Boolean
	| FLOAT						# Float
	| STRING						# String
	| INT							# Int
	| RUNE							# Rune
	| 'new' ID '(' args? ')'		# New
	| ID							# Identifier
	| '(' expr ')'			# Parens;
	
call: '(' args? ')' # FuncCall | '.' ID # Get | '.' ID '(' args? ')' # StructFuncCall;
args: expr (',' expr)*;	
matrixRow: '{' expr (',' expr)* '}';
structField: ID ':' expr;

INT: [0-9]+;
BOOL: 'true' | 'false';
FLOAT: [0-9]+ '.' [0-9]+;
STRING: '"' (ESC_SEQ | ~["\\])* '"';
RUNE: '\'' (ESC_SEQ | ~['\\]) '\'';
WS: [ \t\r\n]+ -> skip;
ID: [a-zA-Z_][a-zA-Z0-9_]*;
COMMENT: '//' ~[\r\n]* -> skip;
ML_COMMENT: '/*' .*? '*/' -> skip;
//Secuencias de escape
ESC_SEQ: '\\' ('n' | 'r' | 't' | 'b' | 'f' | '"' | '\'' | '\\') -> skip;

