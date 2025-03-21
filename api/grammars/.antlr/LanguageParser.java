// Generated from /home/fernando-vicente/Escritorio/Repositorio Local/OLC2_Proyecto1_202111576/api/grammars/Language.g4 by ANTLR 4.13.1
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast", "CheckReturnValue"})
public class LanguageParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.13.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, T__55=56, T__56=57, INT=58, BOOL=59, FLOAT=60, 
		STRING=61, RUNE=62, WS=63, ID=64, COMMENT=65, ML_COMMENT=66, ESC_SEQ=67;
	public static final int
		RULE_program = 0, RULE_dcl = 1, RULE_varDcl = 2, RULE_type = 3, RULE_funcDcl = 4, 
		RULE_classDcl = 5, RULE_classBody = 6, RULE_params = 7, RULE_stmt = 8, 
		RULE_switchCase = 9, RULE_caseStmt = 10, RULE_defaultStmt = 11, RULE_forInit = 12, 
		RULE_expr = 13, RULE_call = 14, RULE_args = 15;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "dcl", "varDcl", "type", "funcDcl", "classDcl", "classBody", 
			"params", "stmt", "switchCase", "caseStmt", "defaultStmt", "forInit", 
			"expr", "call", "args"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'var'", "':'", "'='", "':='", "'int'", "'float'", "'bool'", "'string'", 
			"'rune'", "'func'", "'('", "')'", "'{'", "'}'", "'class'", "','", "';'", 
			"'fmt.Println('", "'if'", "'else if'", "'else'", "'while'", "'for'", 
			"'do'", "'switch'", "'case'", "'default'", "'break'", "'continue'", "'return'", 
			"'-'", "'!'", "'*'", "'/'", "'%'", "'+'", "'>'", "'<'", "'>='", "'<='", 
			"'=='", "'!='", "'+='", "'-='", "'&&'", "'||'", "'++'", "'--'", "'[]'", 
			"'['", "']'", "'slices.Index'", "'strings.Join'", "'len'", "'append'", 
			"'new'", "'.'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, "INT", "BOOL", 
			"FLOAT", "STRING", "RUNE", "WS", "ID", "COMMENT", "ML_COMMENT", "ESC_SEQ"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "Language.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public LanguageParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ProgramContext extends ParserRuleContext {
		public List<DclContext> dcl() {
			return getRuleContexts(DclContext.class);
		}
		public DclContext dcl(int i) {
			return getRuleContext(DclContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(35);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 1)) & ~0x3f) == 0 && ((1L << (_la - 1)) & -4685713933009005055L) != 0)) {
				{
				{
				setState(32);
				dcl();
				}
				}
				setState(37);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class DclContext extends ParserRuleContext {
		public VarDclContext varDcl() {
			return getRuleContext(VarDclContext.class,0);
		}
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public FuncDclContext funcDcl() {
			return getRuleContext(FuncDclContext.class,0);
		}
		public ClassDclContext classDcl() {
			return getRuleContext(ClassDclContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public DclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_dcl; }
	}

	public final DclContext dcl() throws RecognitionException {
		DclContext _localctx = new DclContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_dcl);
		try {
			setState(43);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,1,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(38);
				varDcl();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(39);
				stmt();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(40);
				funcDcl();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(41);
				classDcl();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(42);
				expr(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class VarDclContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(LanguageParser.ID, 0); }
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public VarDclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_varDcl; }
	}

	public final VarDclContext varDcl() throws RecognitionException {
		VarDclContext _localctx = new VarDclContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_varDcl);
		int _la;
		try {
			setState(60);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,3,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(45);
				match(T__0);
				setState(46);
				match(ID);
				setState(47);
				match(T__1);
				setState(48);
				type();
				setState(51);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__2) {
					{
					setState(49);
					match(T__2);
					setState(50);
					expr(0);
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(53);
				match(T__0);
				setState(54);
				match(ID);
				setState(55);
				match(T__2);
				setState(56);
				expr(0);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(57);
				match(ID);
				setState(58);
				match(T__3);
				setState(59);
				expr(0);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class TypeContext extends ParserRuleContext {
		public TypeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type; }
	}

	public final TypeContext type() throws RecognitionException {
		TypeContext _localctx = new TypeContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(62);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 992L) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class FuncDclContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(LanguageParser.ID, 0); }
		public ParamsContext params() {
			return getRuleContext(ParamsContext.class,0);
		}
		public List<DclContext> dcl() {
			return getRuleContexts(DclContext.class);
		}
		public DclContext dcl(int i) {
			return getRuleContext(DclContext.class,i);
		}
		public FuncDclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_funcDcl; }
	}

	public final FuncDclContext funcDcl() throws RecognitionException {
		FuncDclContext _localctx = new FuncDclContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_funcDcl);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(64);
			match(T__9);
			setState(65);
			match(ID);
			setState(66);
			match(T__10);
			setState(68);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0 || _la==ID) {
				{
				setState(67);
				params();
				}
			}

			setState(70);
			match(T__11);
			setState(71);
			match(T__12);
			setState(75);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 1)) & ~0x3f) == 0 && ((1L << (_la - 1)) & -4685713933009005055L) != 0)) {
				{
				{
				setState(72);
				dcl();
				}
				}
				setState(77);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(78);
			match(T__13);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ClassDclContext extends ParserRuleContext {
		public TerminalNode ID() { return getToken(LanguageParser.ID, 0); }
		public List<ClassBodyContext> classBody() {
			return getRuleContexts(ClassBodyContext.class);
		}
		public ClassBodyContext classBody(int i) {
			return getRuleContext(ClassBodyContext.class,i);
		}
		public ClassDclContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_classDcl; }
	}

	public final ClassDclContext classDcl() throws RecognitionException {
		ClassDclContext _localctx = new ClassDclContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_classDcl);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(80);
			match(T__14);
			setState(81);
			match(ID);
			setState(82);
			match(T__12);
			setState(86);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 1)) & ~0x3f) == 0 && ((1L << (_la - 1)) & -9223372036854775295L) != 0)) {
				{
				{
				setState(83);
				classBody();
				}
				}
				setState(88);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(89);
			match(T__13);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ClassBodyContext extends ParserRuleContext {
		public VarDclContext varDcl() {
			return getRuleContext(VarDclContext.class,0);
		}
		public FuncDclContext funcDcl() {
			return getRuleContext(FuncDclContext.class,0);
		}
		public ClassBodyContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_classBody; }
	}

	public final ClassBodyContext classBody() throws RecognitionException {
		ClassBodyContext _localctx = new ClassBodyContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_classBody);
		try {
			setState(93);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(91);
				varDcl();
				}
				break;
			case T__9:
				enterOuterAlt(_localctx, 2);
				{
				setState(92);
				funcDcl();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ParamsContext extends ParserRuleContext {
		public List<TerminalNode> ID() { return getTokens(LanguageParser.ID); }
		public TerminalNode ID(int i) {
			return getToken(LanguageParser.ID, i);
		}
		public List<TypeContext> type() {
			return getRuleContexts(TypeContext.class);
		}
		public TypeContext type(int i) {
			return getRuleContext(TypeContext.class,i);
		}
		public ParamsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_params; }
	}

	public final ParamsContext params() throws RecognitionException {
		ParamsContext _localctx = new ParamsContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_params);
		int _la;
		try {
			setState(117);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case ID:
				enterOuterAlt(_localctx, 1);
				{
				setState(95);
				match(ID);
				setState(100);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__15) {
					{
					{
					setState(96);
					match(T__15);
					setState(97);
					match(ID);
					}
					}
					setState(102);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			case T__0:
				enterOuterAlt(_localctx, 2);
				{
				setState(103);
				match(T__0);
				setState(104);
				match(ID);
				setState(105);
				match(T__1);
				setState(106);
				type();
				setState(114);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__15) {
					{
					{
					setState(107);
					match(T__15);
					setState(108);
					match(T__0);
					setState(109);
					match(ID);
					setState(110);
					match(T__1);
					setState(111);
					type();
					}
					}
					setState(116);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class StmtContext extends ParserRuleContext {
		public StmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_stmt; }
	 
		public StmtContext() { }
		public void copyFrom(StmtContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class SwitchStmtContext extends StmtContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<SwitchCaseContext> switchCase() {
			return getRuleContexts(SwitchCaseContext.class);
		}
		public SwitchCaseContext switchCase(int i) {
			return getRuleContext(SwitchCaseContext.class,i);
		}
		public SwitchStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class PrintStmtContext extends StmtContext {
		public ArgsContext args() {
			return getRuleContext(ArgsContext.class,0);
		}
		public PrintStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class CaseStatementContext extends StmtContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public CaseStatementContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class BlockStmtContext extends StmtContext {
		public List<DclContext> dcl() {
			return getRuleContexts(DclContext.class);
		}
		public DclContext dcl(int i) {
			return getRuleContext(DclContext.class,i);
		}
		public BlockStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class DefaultCaseStmtContext extends StmtContext {
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public DefaultCaseStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ForConditionStmtContext extends StmtContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<StmtContext> stmt() {
			return getRuleContexts(StmtContext.class);
		}
		public StmtContext stmt(int i) {
			return getRuleContext(StmtContext.class,i);
		}
		public ForConditionStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class DoWhileStmtContext extends StmtContext {
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public DoWhileStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ContinueStmtContext extends StmtContext {
		public ContinueStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IfStmtContext extends StmtContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public List<StmtContext> stmt() {
			return getRuleContexts(StmtContext.class);
		}
		public StmtContext stmt(int i) {
			return getRuleContext(StmtContext.class,i);
		}
		public IfStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ExprStmtContext extends StmtContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ExprStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class WhileStmtContext extends StmtContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public WhileStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class BreakStmtContext extends StmtContext {
		public BreakStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ForStmtContext extends StmtContext {
		public ForInitContext forInit() {
			return getRuleContext(ForInitContext.class,0);
		}
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public ForStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ReturnStmtContext extends StmtContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ReturnStmtContext(StmtContext ctx) { copyFrom(ctx); }
	}

	public final StmtContext stmt() throws RecognitionException {
		StmtContext _localctx = new StmtContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_stmt);
		int _la;
		try {
			int _alt;
			setState(205);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,18,_ctx) ) {
			case 1:
				_localctx = new ExprStmtContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(119);
				expr(0);
				setState(120);
				match(T__16);
				}
				break;
			case 2:
				_localctx = new PrintStmtContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(122);
				match(T__17);
				setState(124);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 11)) & ~0x3f) == 0 && ((1L << (_la - 11)) & 13438505995730945L) != 0)) {
					{
					setState(123);
					args();
					}
				}

				setState(126);
				match(T__11);
				}
				break;
			case 3:
				_localctx = new BlockStmtContext(_localctx);
				enterOuterAlt(_localctx, 3);
				{
				setState(127);
				match(T__12);
				setState(131);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (((((_la - 1)) & ~0x3f) == 0 && ((1L << (_la - 1)) & -4685713933009005055L) != 0)) {
					{
					{
					setState(128);
					dcl();
					}
					}
					setState(133);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(134);
				match(T__13);
				}
				break;
			case 4:
				_localctx = new IfStmtContext(_localctx);
				enterOuterAlt(_localctx, 4);
				{
				setState(135);
				match(T__18);
				setState(136);
				expr(0);
				setState(137);
				stmt();
				setState(144);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,13,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(138);
						match(T__19);
						setState(139);
						expr(0);
						setState(140);
						stmt();
						}
						} 
					}
					setState(146);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,13,_ctx);
				}
				setState(149);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
				case 1:
					{
					setState(147);
					match(T__20);
					setState(148);
					stmt();
					}
					break;
				}
				}
				break;
			case 5:
				_localctx = new WhileStmtContext(_localctx);
				enterOuterAlt(_localctx, 5);
				{
				setState(151);
				match(T__21);
				setState(152);
				match(T__10);
				setState(153);
				expr(0);
				setState(154);
				match(T__11);
				setState(155);
				stmt();
				}
				break;
			case 6:
				_localctx = new ForConditionStmtContext(_localctx);
				enterOuterAlt(_localctx, 6);
				{
				setState(157);
				match(T__22);
				setState(158);
				expr(0);
				setState(159);
				match(T__12);
				setState(163);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (((((_la - 11)) & ~0x3f) == 0 && ((1L << (_la - 11)) & 13438505996777861L) != 0)) {
					{
					{
					setState(160);
					stmt();
					}
					}
					setState(165);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(166);
				match(T__13);
				}
				break;
			case 7:
				_localctx = new ForStmtContext(_localctx);
				enterOuterAlt(_localctx, 7);
				{
				setState(168);
				match(T__22);
				setState(169);
				forInit();
				setState(170);
				expr(0);
				setState(171);
				match(T__16);
				setState(172);
				expr(0);
				setState(173);
				stmt();
				}
				break;
			case 8:
				_localctx = new DoWhileStmtContext(_localctx);
				enterOuterAlt(_localctx, 8);
				{
				setState(175);
				match(T__23);
				setState(176);
				stmt();
				setState(177);
				match(T__21);
				setState(178);
				match(T__10);
				setState(179);
				expr(0);
				setState(180);
				match(T__11);
				}
				break;
			case 9:
				_localctx = new SwitchStmtContext(_localctx);
				enterOuterAlt(_localctx, 9);
				{
				setState(182);
				match(T__24);
				setState(183);
				expr(0);
				setState(184);
				match(T__12);
				setState(188);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__25 || _la==T__26) {
					{
					{
					setState(185);
					switchCase();
					}
					}
					setState(190);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(191);
				match(T__13);
				}
				break;
			case 10:
				_localctx = new CaseStatementContext(_localctx);
				enterOuterAlt(_localctx, 10);
				{
				setState(193);
				match(T__25);
				setState(194);
				expr(0);
				setState(195);
				stmt();
				}
				break;
			case 11:
				_localctx = new DefaultCaseStmtContext(_localctx);
				enterOuterAlt(_localctx, 11);
				{
				setState(197);
				match(T__26);
				setState(198);
				stmt();
				}
				break;
			case 12:
				_localctx = new BreakStmtContext(_localctx);
				enterOuterAlt(_localctx, 12);
				{
				setState(199);
				match(T__27);
				}
				break;
			case 13:
				_localctx = new ContinueStmtContext(_localctx);
				enterOuterAlt(_localctx, 13);
				{
				setState(200);
				match(T__28);
				}
				break;
			case 14:
				_localctx = new ReturnStmtContext(_localctx);
				enterOuterAlt(_localctx, 14);
				{
				setState(201);
				match(T__29);
				setState(203);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
				case 1:
					{
					setState(202);
					expr(0);
					}
					break;
				}
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class SwitchCaseContext extends ParserRuleContext {
		public CaseStmtContext caseStmt() {
			return getRuleContext(CaseStmtContext.class,0);
		}
		public DefaultStmtContext defaultStmt() {
			return getRuleContext(DefaultStmtContext.class,0);
		}
		public SwitchCaseContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_switchCase; }
	}

	public final SwitchCaseContext switchCase() throws RecognitionException {
		SwitchCaseContext _localctx = new SwitchCaseContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_switchCase);
		try {
			setState(209);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__25:
				enterOuterAlt(_localctx, 1);
				{
				setState(207);
				caseStmt();
				}
				break;
			case T__26:
				enterOuterAlt(_localctx, 2);
				{
				setState(208);
				defaultStmt();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CaseStmtContext extends ParserRuleContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public CaseStmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_caseStmt; }
	}

	public final CaseStmtContext caseStmt() throws RecognitionException {
		CaseStmtContext _localctx = new CaseStmtContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_caseStmt);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(211);
			match(T__25);
			setState(212);
			expr(0);
			setState(213);
			match(T__1);
			setState(214);
			stmt();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class DefaultStmtContext extends ParserRuleContext {
		public StmtContext stmt() {
			return getRuleContext(StmtContext.class,0);
		}
		public DefaultStmtContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_defaultStmt; }
	}

	public final DefaultStmtContext defaultStmt() throws RecognitionException {
		DefaultStmtContext _localctx = new DefaultStmtContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_defaultStmt);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(216);
			match(T__26);
			setState(217);
			match(T__1);
			setState(218);
			stmt();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ForInitContext extends ParserRuleContext {
		public VarDclContext varDcl() {
			return getRuleContext(VarDclContext.class,0);
		}
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ForInitContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_forInit; }
	}

	public final ForInitContext forInit() throws RecognitionException {
		ForInitContext _localctx = new ForInitContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_forInit);
		try {
			setState(224);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(220);
				varDcl();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(221);
				expr(0);
				setState(222);
				match(T__16);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ExprContext extends ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expr; }
	 
		public ExprContext() { }
		public void copyFrom(ExprContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class CalleeContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public List<CallContext> call() {
			return getRuleContexts(CallContext.class);
		}
		public CallContext call(int i) {
			return getRuleContext(CallContext.class,i);
		}
		public CalleeContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class SliceIndexCallContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public SliceIndexCallContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NewContext extends ExprContext {
		public TerminalNode ID() { return getToken(LanguageParser.ID, 0); }
		public ArgsContext args() {
			return getRuleContext(ArgsContext.class,0);
		}
		public NewContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class MulDivContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public MulDivContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class ParensContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public ParensContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StringsJoinCallContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public StringsJoinCallContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class LogicalContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public LogicalContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class StringContext extends ExprContext {
		public TerminalNode STRING() { return getToken(LanguageParser.STRING, 0); }
		public StringContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IntContext extends ExprContext {
		public TerminalNode INT() { return getToken(LanguageParser.INT, 0); }
		public IntContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class LenCallContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public LenCallContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IdentifierContext extends ExprContext {
		public TerminalNode ID() { return getToken(LanguageParser.ID, 0); }
		public IdentifierContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class EqualityContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public EqualityContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class BooleanContext extends ExprContext {
		public TerminalNode BOOL() { return getToken(LanguageParser.BOOL, 0); }
		public BooleanContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class SliceInitContext extends ExprContext {
		public TypeContext type() {
			return getRuleContext(TypeContext.class,0);
		}
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public SliceInitContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AssignOpContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public AssignOpContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class SliceAccessContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public SliceAccessContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AddSubContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public AddSubContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class RelationalContext extends ExprContext {
		public Token op;
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public RelationalContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class IncDecContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public IncDecContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class FloatContext extends ExprContext {
		public TerminalNode FLOAT() { return getToken(LanguageParser.FLOAT, 0); }
		public FloatContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NotContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public NotContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class SliceRangeContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public SliceRangeContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AssignContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public AssignContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class NegateContext extends ExprContext {
		public ExprContext expr() {
			return getRuleContext(ExprContext.class,0);
		}
		public NegateContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class AppendCallContext extends ExprContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public AppendCallContext(ExprContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class RuneContext extends ExprContext {
		public TerminalNode RUNE() { return getToken(LanguageParser.RUNE, 0); }
		public RuneContext(ExprContext ctx) { copyFrom(ctx); }
	}

	public final ExprContext expr() throws RecognitionException {
		return expr(0);
	}

	private ExprContext expr(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExprContext _localctx = new ExprContext(_ctx, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 26;
		enterRecursionRule(_localctx, 26, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(294);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__30:
				{
				_localctx = new NegateContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;

				setState(227);
				match(T__30);
				setState(228);
				expr(26);
				}
				break;
			case T__31:
				{
				_localctx = new NotContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(229);
				match(T__31);
				setState(230);
				expr(25);
				}
				break;
			case T__48:
				{
				_localctx = new SliceInitContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(231);
				match(T__48);
				setState(232);
				type();
				setState(233);
				match(T__12);
				setState(234);
				expr(0);
				setState(239);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__15) {
					{
					{
					setState(235);
					match(T__15);
					setState(236);
					expr(0);
					}
					}
					setState(241);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(242);
				match(T__13);
				}
				break;
			case T__51:
				{
				_localctx = new SliceIndexCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(244);
				match(T__51);
				setState(245);
				match(T__10);
				setState(246);
				expr(0);
				setState(247);
				match(T__15);
				setState(248);
				expr(0);
				setState(249);
				match(T__11);
				}
				break;
			case T__52:
				{
				_localctx = new StringsJoinCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(251);
				match(T__52);
				setState(252);
				match(T__10);
				setState(253);
				expr(0);
				setState(254);
				match(T__15);
				setState(255);
				expr(0);
				setState(256);
				match(T__11);
				}
				break;
			case T__53:
				{
				_localctx = new LenCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(258);
				match(T__53);
				setState(259);
				match(T__10);
				setState(260);
				expr(0);
				setState(261);
				match(T__11);
				}
				break;
			case T__54:
				{
				_localctx = new AppendCallContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(263);
				match(T__54);
				setState(264);
				match(T__10);
				setState(265);
				expr(0);
				setState(266);
				match(T__15);
				setState(267);
				expr(0);
				setState(272);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==T__15) {
					{
					{
					setState(268);
					match(T__15);
					setState(269);
					expr(0);
					}
					}
					setState(274);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(275);
				match(T__11);
				}
				break;
			case BOOL:
				{
				_localctx = new BooleanContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(277);
				match(BOOL);
				}
				break;
			case FLOAT:
				{
				_localctx = new FloatContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(278);
				match(FLOAT);
				}
				break;
			case STRING:
				{
				_localctx = new StringContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(279);
				match(STRING);
				}
				break;
			case INT:
				{
				_localctx = new IntContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(280);
				match(INT);
				}
				break;
			case RUNE:
				{
				_localctx = new RuneContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(281);
				match(RUNE);
				}
				break;
			case T__55:
				{
				_localctx = new NewContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(282);
				match(T__55);
				setState(283);
				match(ID);
				setState(284);
				match(T__10);
				setState(286);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 11)) & ~0x3f) == 0 && ((1L << (_la - 11)) & 13438505995730945L) != 0)) {
					{
					setState(285);
					args();
					}
				}

				setState(288);
				match(T__11);
				}
				break;
			case ID:
				{
				_localctx = new IdentifierContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(289);
				match(ID);
				}
				break;
			case T__10:
				{
				_localctx = new ParensContext(_localctx);
				_ctx = _localctx;
				_prevctx = _localctx;
				setState(290);
				match(T__10);
				setState(291);
				expr(0);
				setState(292);
				match(T__11);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			_ctx.stop = _input.LT(-1);
			setState(339);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,27,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(337);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,26,_ctx) ) {
					case 1:
						{
						_localctx = new MulDivContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(296);
						if (!(precpred(_ctx, 23))) throw new FailedPredicateException(this, "precpred(_ctx, 23)");
						setState(297);
						((MulDivContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 60129542144L) != 0)) ) {
							((MulDivContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(298);
						expr(24);
						}
						break;
					case 2:
						{
						_localctx = new AddSubContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(299);
						if (!(precpred(_ctx, 22))) throw new FailedPredicateException(this, "precpred(_ctx, 22)");
						setState(300);
						((AddSubContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__30 || _la==T__35) ) {
							((AddSubContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(301);
						expr(23);
						}
						break;
					case 3:
						{
						_localctx = new RelationalContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(302);
						if (!(precpred(_ctx, 21))) throw new FailedPredicateException(this, "precpred(_ctx, 21)");
						setState(303);
						((RelationalContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & 2061584302080L) != 0)) ) {
							((RelationalContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(304);
						expr(22);
						}
						break;
					case 4:
						{
						_localctx = new EqualityContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(305);
						if (!(precpred(_ctx, 20))) throw new FailedPredicateException(this, "precpred(_ctx, 20)");
						setState(306);
						((EqualityContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__40 || _la==T__41) ) {
							((EqualityContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(307);
						expr(21);
						}
						break;
					case 5:
						{
						_localctx = new AssignOpContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(308);
						if (!(precpred(_ctx, 19))) throw new FailedPredicateException(this, "precpred(_ctx, 19)");
						setState(309);
						((AssignOpContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__42 || _la==T__43) ) {
							((AssignOpContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(310);
						expr(20);
						}
						break;
					case 6:
						{
						_localctx = new LogicalContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(311);
						if (!(precpred(_ctx, 18))) throw new FailedPredicateException(this, "precpred(_ctx, 18)");
						setState(312);
						((LogicalContext)_localctx).op = _input.LT(1);
						_la = _input.LA(1);
						if ( !(_la==T__44 || _la==T__45) ) {
							((LogicalContext)_localctx).op = (Token)_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(313);
						expr(19);
						}
						break;
					case 7:
						{
						_localctx = new AssignContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(314);
						if (!(precpred(_ctx, 17))) throw new FailedPredicateException(this, "precpred(_ctx, 17)");
						setState(315);
						_la = _input.LA(1);
						if ( !(_la==T__2 || _la==T__3) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						setState(316);
						expr(18);
						}
						break;
					case 8:
						{
						_localctx = new CalleeContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(317);
						if (!(precpred(_ctx, 24))) throw new FailedPredicateException(this, "precpred(_ctx, 24)");
						setState(319); 
						_errHandler.sync(this);
						_alt = 1;
						do {
							switch (_alt) {
							case 1:
								{
								{
								setState(318);
								call();
								}
								}
								break;
							default:
								throw new NoViableAltException(this);
							}
							setState(321); 
							_errHandler.sync(this);
							_alt = getInterpreter().adaptivePredict(_input,25,_ctx);
						} while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER );
						}
						break;
					case 9:
						{
						_localctx = new IncDecContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(323);
						if (!(precpred(_ctx, 16))) throw new FailedPredicateException(this, "precpred(_ctx, 16)");
						setState(324);
						_la = _input.LA(1);
						if ( !(_la==T__46 || _la==T__47) ) {
						_errHandler.recoverInline(this);
						}
						else {
							if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
							_errHandler.reportMatch(this);
							consume();
						}
						}
						break;
					case 10:
						{
						_localctx = new SliceAccessContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(325);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(326);
						match(T__49);
						setState(327);
						expr(0);
						setState(328);
						match(T__50);
						}
						break;
					case 11:
						{
						_localctx = new SliceRangeContext(new ExprContext(_parentctx, _parentState));
						pushNewRecursionContext(_localctx, _startState, RULE_expr);
						setState(330);
						if (!(precpred(_ctx, 13))) throw new FailedPredicateException(this, "precpred(_ctx, 13)");
						setState(331);
						match(T__49);
						setState(332);
						expr(0);
						setState(333);
						match(T__1);
						setState(334);
						expr(0);
						setState(335);
						match(T__50);
						}
						break;
					}
					} 
				}
				setState(341);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,27,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class CallContext extends ParserRuleContext {
		public CallContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_call; }
	 
		public CallContext() { }
		public void copyFrom(CallContext ctx) {
			super.copyFrom(ctx);
		}
	}
	@SuppressWarnings("CheckReturnValue")
	public static class FuncCallContext extends CallContext {
		public ArgsContext args() {
			return getRuleContext(ArgsContext.class,0);
		}
		public FuncCallContext(CallContext ctx) { copyFrom(ctx); }
	}
	@SuppressWarnings("CheckReturnValue")
	public static class GetContext extends CallContext {
		public TerminalNode ID() { return getToken(LanguageParser.ID, 0); }
		public GetContext(CallContext ctx) { copyFrom(ctx); }
	}

	public final CallContext call() throws RecognitionException {
		CallContext _localctx = new CallContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_call);
		int _la;
		try {
			setState(349);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__10:
				_localctx = new FuncCallContext(_localctx);
				enterOuterAlt(_localctx, 1);
				{
				setState(342);
				match(T__10);
				setState(344);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 11)) & ~0x3f) == 0 && ((1L << (_la - 11)) & 13438505995730945L) != 0)) {
					{
					setState(343);
					args();
					}
				}

				setState(346);
				match(T__11);
				}
				break;
			case T__56:
				_localctx = new GetContext(_localctx);
				enterOuterAlt(_localctx, 2);
				{
				setState(347);
				match(T__56);
				setState(348);
				match(ID);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	@SuppressWarnings("CheckReturnValue")
	public static class ArgsContext extends ParserRuleContext {
		public List<ExprContext> expr() {
			return getRuleContexts(ExprContext.class);
		}
		public ExprContext expr(int i) {
			return getRuleContext(ExprContext.class,i);
		}
		public ArgsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_args; }
	}

	public final ArgsContext args() throws RecognitionException {
		ArgsContext _localctx = new ArgsContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_args);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(351);
			expr(0);
			setState(356);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__15) {
				{
				{
				setState(352);
				match(T__15);
				setState(353);
				expr(0);
				}
				}
				setState(358);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 13:
			return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 23);
		case 1:
			return precpred(_ctx, 22);
		case 2:
			return precpred(_ctx, 21);
		case 3:
			return precpred(_ctx, 20);
		case 4:
			return precpred(_ctx, 19);
		case 5:
			return precpred(_ctx, 18);
		case 6:
			return precpred(_ctx, 17);
		case 7:
			return precpred(_ctx, 24);
		case 8:
			return precpred(_ctx, 16);
		case 9:
			return precpred(_ctx, 14);
		case 10:
			return precpred(_ctx, 13);
		}
		return true;
	}

	public static final String _serializedATN =
		"\u0004\u0001C\u0168\u0002\u0000\u0007\u0000\u0002\u0001\u0007\u0001\u0002"+
		"\u0002\u0007\u0002\u0002\u0003\u0007\u0003\u0002\u0004\u0007\u0004\u0002"+
		"\u0005\u0007\u0005\u0002\u0006\u0007\u0006\u0002\u0007\u0007\u0007\u0002"+
		"\b\u0007\b\u0002\t\u0007\t\u0002\n\u0007\n\u0002\u000b\u0007\u000b\u0002"+
		"\f\u0007\f\u0002\r\u0007\r\u0002\u000e\u0007\u000e\u0002\u000f\u0007\u000f"+
		"\u0001\u0000\u0005\u0000\"\b\u0000\n\u0000\f\u0000%\t\u0000\u0001\u0001"+
		"\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0001\u0003\u0001,\b\u0001"+
		"\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0003\u00024\b\u0002\u0001\u0002\u0001\u0002\u0001\u0002\u0001\u0002"+
		"\u0001\u0002\u0001\u0002\u0001\u0002\u0003\u0002=\b\u0002\u0001\u0003"+
		"\u0001\u0003\u0001\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0003\u0004"+
		"E\b\u0004\u0001\u0004\u0001\u0004\u0001\u0004\u0005\u0004J\b\u0004\n\u0004"+
		"\f\u0004M\t\u0004\u0001\u0004\u0001\u0004\u0001\u0005\u0001\u0005\u0001"+
		"\u0005\u0001\u0005\u0005\u0005U\b\u0005\n\u0005\f\u0005X\t\u0005\u0001"+
		"\u0005\u0001\u0005\u0001\u0006\u0001\u0006\u0003\u0006^\b\u0006\u0001"+
		"\u0007\u0001\u0007\u0001\u0007\u0005\u0007c\b\u0007\n\u0007\f\u0007f\t"+
		"\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0001"+
		"\u0007\u0001\u0007\u0001\u0007\u0001\u0007\u0005\u0007q\b\u0007\n\u0007"+
		"\f\u0007t\t\u0007\u0003\u0007v\b\u0007\u0001\b\u0001\b\u0001\b\u0001\b"+
		"\u0001\b\u0003\b}\b\b\u0001\b\u0001\b\u0001\b\u0005\b\u0082\b\b\n\b\f"+
		"\b\u0085\t\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001"+
		"\b\u0005\b\u008f\b\b\n\b\f\b\u0092\t\b\u0001\b\u0001\b\u0003\b\u0096\b"+
		"\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001"+
		"\b\u0001\b\u0005\b\u00a2\b\b\n\b\f\b\u00a5\t\b\u0001\b\u0001\b\u0001\b"+
		"\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001"+
		"\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0005"+
		"\b\u00bb\b\b\n\b\f\b\u00be\t\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b"+
		"\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0001\b\u0003\b\u00cc"+
		"\b\b\u0003\b\u00ce\b\b\u0001\t\u0001\t\u0003\t\u00d2\b\t\u0001\n\u0001"+
		"\n\u0001\n\u0001\n\u0001\n\u0001\u000b\u0001\u000b\u0001\u000b\u0001\u000b"+
		"\u0001\f\u0001\f\u0001\f\u0001\f\u0003\f\u00e1\b\f\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0005"+
		"\r\u00ee\b\r\n\r\f\r\u00f1\t\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r"+
		"\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0005\r\u010f\b\r\n\r\f\r\u0112"+
		"\t\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0001\r\u0003\r\u011f\b\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0003\r\u0127\b\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0004"+
		"\r\u0140\b\r\u000b\r\f\r\u0141\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r"+
		"\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001\r\u0001"+
		"\r\u0005\r\u0152\b\r\n\r\f\r\u0155\t\r\u0001\u000e\u0001\u000e\u0003\u000e"+
		"\u0159\b\u000e\u0001\u000e\u0001\u000e\u0001\u000e\u0003\u000e\u015e\b"+
		"\u000e\u0001\u000f\u0001\u000f\u0001\u000f\u0005\u000f\u0163\b\u000f\n"+
		"\u000f\f\u000f\u0166\t\u000f\u0001\u000f\u0000\u0001\u001a\u0010\u0000"+
		"\u0002\u0004\u0006\b\n\f\u000e\u0010\u0012\u0014\u0016\u0018\u001a\u001c"+
		"\u001e\u0000\t\u0001\u0000\u0005\t\u0001\u0000!#\u0002\u0000\u001f\u001f"+
		"$$\u0001\u0000%(\u0001\u0000)*\u0001\u0000+,\u0001\u0000-.\u0001\u0000"+
		"\u0003\u0004\u0001\u0000/0\u019c\u0000#\u0001\u0000\u0000\u0000\u0002"+
		"+\u0001\u0000\u0000\u0000\u0004<\u0001\u0000\u0000\u0000\u0006>\u0001"+
		"\u0000\u0000\u0000\b@\u0001\u0000\u0000\u0000\nP\u0001\u0000\u0000\u0000"+
		"\f]\u0001\u0000\u0000\u0000\u000eu\u0001\u0000\u0000\u0000\u0010\u00cd"+
		"\u0001\u0000\u0000\u0000\u0012\u00d1\u0001\u0000\u0000\u0000\u0014\u00d3"+
		"\u0001\u0000\u0000\u0000\u0016\u00d8\u0001\u0000\u0000\u0000\u0018\u00e0"+
		"\u0001\u0000\u0000\u0000\u001a\u0126\u0001\u0000\u0000\u0000\u001c\u015d"+
		"\u0001\u0000\u0000\u0000\u001e\u015f\u0001\u0000\u0000\u0000 \"\u0003"+
		"\u0002\u0001\u0000! \u0001\u0000\u0000\u0000\"%\u0001\u0000\u0000\u0000"+
		"#!\u0001\u0000\u0000\u0000#$\u0001\u0000\u0000\u0000$\u0001\u0001\u0000"+
		"\u0000\u0000%#\u0001\u0000\u0000\u0000&,\u0003\u0004\u0002\u0000\',\u0003"+
		"\u0010\b\u0000(,\u0003\b\u0004\u0000),\u0003\n\u0005\u0000*,\u0003\u001a"+
		"\r\u0000+&\u0001\u0000\u0000\u0000+\'\u0001\u0000\u0000\u0000+(\u0001"+
		"\u0000\u0000\u0000+)\u0001\u0000\u0000\u0000+*\u0001\u0000\u0000\u0000"+
		",\u0003\u0001\u0000\u0000\u0000-.\u0005\u0001\u0000\u0000./\u0005@\u0000"+
		"\u0000/0\u0005\u0002\u0000\u000003\u0003\u0006\u0003\u000012\u0005\u0003"+
		"\u0000\u000024\u0003\u001a\r\u000031\u0001\u0000\u0000\u000034\u0001\u0000"+
		"\u0000\u00004=\u0001\u0000\u0000\u000056\u0005\u0001\u0000\u000067\u0005"+
		"@\u0000\u000078\u0005\u0003\u0000\u00008=\u0003\u001a\r\u00009:\u0005"+
		"@\u0000\u0000:;\u0005\u0004\u0000\u0000;=\u0003\u001a\r\u0000<-\u0001"+
		"\u0000\u0000\u0000<5\u0001\u0000\u0000\u0000<9\u0001\u0000\u0000\u0000"+
		"=\u0005\u0001\u0000\u0000\u0000>?\u0007\u0000\u0000\u0000?\u0007\u0001"+
		"\u0000\u0000\u0000@A\u0005\n\u0000\u0000AB\u0005@\u0000\u0000BD\u0005"+
		"\u000b\u0000\u0000CE\u0003\u000e\u0007\u0000DC\u0001\u0000\u0000\u0000"+
		"DE\u0001\u0000\u0000\u0000EF\u0001\u0000\u0000\u0000FG\u0005\f\u0000\u0000"+
		"GK\u0005\r\u0000\u0000HJ\u0003\u0002\u0001\u0000IH\u0001\u0000\u0000\u0000"+
		"JM\u0001\u0000\u0000\u0000KI\u0001\u0000\u0000\u0000KL\u0001\u0000\u0000"+
		"\u0000LN\u0001\u0000\u0000\u0000MK\u0001\u0000\u0000\u0000NO\u0005\u000e"+
		"\u0000\u0000O\t\u0001\u0000\u0000\u0000PQ\u0005\u000f\u0000\u0000QR\u0005"+
		"@\u0000\u0000RV\u0005\r\u0000\u0000SU\u0003\f\u0006\u0000TS\u0001\u0000"+
		"\u0000\u0000UX\u0001\u0000\u0000\u0000VT\u0001\u0000\u0000\u0000VW\u0001"+
		"\u0000\u0000\u0000WY\u0001\u0000\u0000\u0000XV\u0001\u0000\u0000\u0000"+
		"YZ\u0005\u000e\u0000\u0000Z\u000b\u0001\u0000\u0000\u0000[^\u0003\u0004"+
		"\u0002\u0000\\^\u0003\b\u0004\u0000][\u0001\u0000\u0000\u0000]\\\u0001"+
		"\u0000\u0000\u0000^\r\u0001\u0000\u0000\u0000_d\u0005@\u0000\u0000`a\u0005"+
		"\u0010\u0000\u0000ac\u0005@\u0000\u0000b`\u0001\u0000\u0000\u0000cf\u0001"+
		"\u0000\u0000\u0000db\u0001\u0000\u0000\u0000de\u0001\u0000\u0000\u0000"+
		"ev\u0001\u0000\u0000\u0000fd\u0001\u0000\u0000\u0000gh\u0005\u0001\u0000"+
		"\u0000hi\u0005@\u0000\u0000ij\u0005\u0002\u0000\u0000jr\u0003\u0006\u0003"+
		"\u0000kl\u0005\u0010\u0000\u0000lm\u0005\u0001\u0000\u0000mn\u0005@\u0000"+
		"\u0000no\u0005\u0002\u0000\u0000oq\u0003\u0006\u0003\u0000pk\u0001\u0000"+
		"\u0000\u0000qt\u0001\u0000\u0000\u0000rp\u0001\u0000\u0000\u0000rs\u0001"+
		"\u0000\u0000\u0000sv\u0001\u0000\u0000\u0000tr\u0001\u0000\u0000\u0000"+
		"u_\u0001\u0000\u0000\u0000ug\u0001\u0000\u0000\u0000v\u000f\u0001\u0000"+
		"\u0000\u0000wx\u0003\u001a\r\u0000xy\u0005\u0011\u0000\u0000y\u00ce\u0001"+
		"\u0000\u0000\u0000z|\u0005\u0012\u0000\u0000{}\u0003\u001e\u000f\u0000"+
		"|{\u0001\u0000\u0000\u0000|}\u0001\u0000\u0000\u0000}~\u0001\u0000\u0000"+
		"\u0000~\u00ce\u0005\f\u0000\u0000\u007f\u0083\u0005\r\u0000\u0000\u0080"+
		"\u0082\u0003\u0002\u0001\u0000\u0081\u0080\u0001\u0000\u0000\u0000\u0082"+
		"\u0085\u0001\u0000\u0000\u0000\u0083\u0081\u0001\u0000\u0000\u0000\u0083"+
		"\u0084\u0001\u0000\u0000\u0000\u0084\u0086\u0001\u0000\u0000\u0000\u0085"+
		"\u0083\u0001\u0000\u0000\u0000\u0086\u00ce\u0005\u000e\u0000\u0000\u0087"+
		"\u0088\u0005\u0013\u0000\u0000\u0088\u0089\u0003\u001a\r\u0000\u0089\u0090"+
		"\u0003\u0010\b\u0000\u008a\u008b\u0005\u0014\u0000\u0000\u008b\u008c\u0003"+
		"\u001a\r\u0000\u008c\u008d\u0003\u0010\b\u0000\u008d\u008f\u0001\u0000"+
		"\u0000\u0000\u008e\u008a\u0001\u0000\u0000\u0000\u008f\u0092\u0001\u0000"+
		"\u0000\u0000\u0090\u008e\u0001\u0000\u0000\u0000\u0090\u0091\u0001\u0000"+
		"\u0000\u0000\u0091\u0095\u0001\u0000\u0000\u0000\u0092\u0090\u0001\u0000"+
		"\u0000\u0000\u0093\u0094\u0005\u0015\u0000\u0000\u0094\u0096\u0003\u0010"+
		"\b\u0000\u0095\u0093\u0001\u0000\u0000\u0000\u0095\u0096\u0001\u0000\u0000"+
		"\u0000\u0096\u00ce\u0001\u0000\u0000\u0000\u0097\u0098\u0005\u0016\u0000"+
		"\u0000\u0098\u0099\u0005\u000b\u0000\u0000\u0099\u009a\u0003\u001a\r\u0000"+
		"\u009a\u009b\u0005\f\u0000\u0000\u009b\u009c\u0003\u0010\b\u0000\u009c"+
		"\u00ce\u0001\u0000\u0000\u0000\u009d\u009e\u0005\u0017\u0000\u0000\u009e"+
		"\u009f\u0003\u001a\r\u0000\u009f\u00a3\u0005\r\u0000\u0000\u00a0\u00a2"+
		"\u0003\u0010\b\u0000\u00a1\u00a0\u0001\u0000\u0000\u0000\u00a2\u00a5\u0001"+
		"\u0000\u0000\u0000\u00a3\u00a1\u0001\u0000\u0000\u0000\u00a3\u00a4\u0001"+
		"\u0000\u0000\u0000\u00a4\u00a6\u0001\u0000\u0000\u0000\u00a5\u00a3\u0001"+
		"\u0000\u0000\u0000\u00a6\u00a7\u0005\u000e\u0000\u0000\u00a7\u00ce\u0001"+
		"\u0000\u0000\u0000\u00a8\u00a9\u0005\u0017\u0000\u0000\u00a9\u00aa\u0003"+
		"\u0018\f\u0000\u00aa\u00ab\u0003\u001a\r\u0000\u00ab\u00ac\u0005\u0011"+
		"\u0000\u0000\u00ac\u00ad\u0003\u001a\r\u0000\u00ad\u00ae\u0003\u0010\b"+
		"\u0000\u00ae\u00ce\u0001\u0000\u0000\u0000\u00af\u00b0\u0005\u0018\u0000"+
		"\u0000\u00b0\u00b1\u0003\u0010\b\u0000\u00b1\u00b2\u0005\u0016\u0000\u0000"+
		"\u00b2\u00b3\u0005\u000b\u0000\u0000\u00b3\u00b4\u0003\u001a\r\u0000\u00b4"+
		"\u00b5\u0005\f\u0000\u0000\u00b5\u00ce\u0001\u0000\u0000\u0000\u00b6\u00b7"+
		"\u0005\u0019\u0000\u0000\u00b7\u00b8\u0003\u001a\r\u0000\u00b8\u00bc\u0005"+
		"\r\u0000\u0000\u00b9\u00bb\u0003\u0012\t\u0000\u00ba\u00b9\u0001\u0000"+
		"\u0000\u0000\u00bb\u00be\u0001\u0000\u0000\u0000\u00bc\u00ba\u0001\u0000"+
		"\u0000\u0000\u00bc\u00bd\u0001\u0000\u0000\u0000\u00bd\u00bf\u0001\u0000"+
		"\u0000\u0000\u00be\u00bc\u0001\u0000\u0000\u0000\u00bf\u00c0\u0005\u000e"+
		"\u0000\u0000\u00c0\u00ce\u0001\u0000\u0000\u0000\u00c1\u00c2\u0005\u001a"+
		"\u0000\u0000\u00c2\u00c3\u0003\u001a\r\u0000\u00c3\u00c4\u0003\u0010\b"+
		"\u0000\u00c4\u00ce\u0001\u0000\u0000\u0000\u00c5\u00c6\u0005\u001b\u0000"+
		"\u0000\u00c6\u00ce\u0003\u0010\b\u0000\u00c7\u00ce\u0005\u001c\u0000\u0000"+
		"\u00c8\u00ce\u0005\u001d\u0000\u0000\u00c9\u00cb\u0005\u001e\u0000\u0000"+
		"\u00ca\u00cc\u0003\u001a\r\u0000\u00cb\u00ca\u0001\u0000\u0000\u0000\u00cb"+
		"\u00cc\u0001\u0000\u0000\u0000\u00cc\u00ce\u0001\u0000\u0000\u0000\u00cd"+
		"w\u0001\u0000\u0000\u0000\u00cdz\u0001\u0000\u0000\u0000\u00cd\u007f\u0001"+
		"\u0000\u0000\u0000\u00cd\u0087\u0001\u0000\u0000\u0000\u00cd\u0097\u0001"+
		"\u0000\u0000\u0000\u00cd\u009d\u0001\u0000\u0000\u0000\u00cd\u00a8\u0001"+
		"\u0000\u0000\u0000\u00cd\u00af\u0001\u0000\u0000\u0000\u00cd\u00b6\u0001"+
		"\u0000\u0000\u0000\u00cd\u00c1\u0001\u0000\u0000\u0000\u00cd\u00c5\u0001"+
		"\u0000\u0000\u0000\u00cd\u00c7\u0001\u0000\u0000\u0000\u00cd\u00c8\u0001"+
		"\u0000\u0000\u0000\u00cd\u00c9\u0001\u0000\u0000\u0000\u00ce\u0011\u0001"+
		"\u0000\u0000\u0000\u00cf\u00d2\u0003\u0014\n\u0000\u00d0\u00d2\u0003\u0016"+
		"\u000b\u0000\u00d1\u00cf\u0001\u0000\u0000\u0000\u00d1\u00d0\u0001\u0000"+
		"\u0000\u0000\u00d2\u0013\u0001\u0000\u0000\u0000\u00d3\u00d4\u0005\u001a"+
		"\u0000\u0000\u00d4\u00d5\u0003\u001a\r\u0000\u00d5\u00d6\u0005\u0002\u0000"+
		"\u0000\u00d6\u00d7\u0003\u0010\b\u0000\u00d7\u0015\u0001\u0000\u0000\u0000"+
		"\u00d8\u00d9\u0005\u001b\u0000\u0000\u00d9\u00da\u0005\u0002\u0000\u0000"+
		"\u00da\u00db\u0003\u0010\b\u0000\u00db\u0017\u0001\u0000\u0000\u0000\u00dc"+
		"\u00e1\u0003\u0004\u0002\u0000\u00dd\u00de\u0003\u001a\r\u0000\u00de\u00df"+
		"\u0005\u0011\u0000\u0000\u00df\u00e1\u0001\u0000\u0000\u0000\u00e0\u00dc"+
		"\u0001\u0000\u0000\u0000\u00e0\u00dd\u0001\u0000\u0000\u0000\u00e1\u0019"+
		"\u0001\u0000\u0000\u0000\u00e2\u00e3\u0006\r\uffff\uffff\u0000\u00e3\u00e4"+
		"\u0005\u001f\u0000\u0000\u00e4\u0127\u0003\u001a\r\u001a\u00e5\u00e6\u0005"+
		" \u0000\u0000\u00e6\u0127\u0003\u001a\r\u0019\u00e7\u00e8\u00051\u0000"+
		"\u0000\u00e8\u00e9\u0003\u0006\u0003\u0000\u00e9\u00ea\u0005\r\u0000\u0000"+
		"\u00ea\u00ef\u0003\u001a\r\u0000\u00eb\u00ec\u0005\u0010\u0000\u0000\u00ec"+
		"\u00ee\u0003\u001a\r\u0000\u00ed\u00eb\u0001\u0000\u0000\u0000\u00ee\u00f1"+
		"\u0001\u0000\u0000\u0000\u00ef\u00ed\u0001\u0000\u0000\u0000\u00ef\u00f0"+
		"\u0001\u0000\u0000\u0000\u00f0\u00f2\u0001\u0000\u0000\u0000\u00f1\u00ef"+
		"\u0001\u0000\u0000\u0000\u00f2\u00f3\u0005\u000e\u0000\u0000\u00f3\u0127"+
		"\u0001\u0000\u0000\u0000\u00f4\u00f5\u00054\u0000\u0000\u00f5\u00f6\u0005"+
		"\u000b\u0000\u0000\u00f6\u00f7\u0003\u001a\r\u0000\u00f7\u00f8\u0005\u0010"+
		"\u0000\u0000\u00f8\u00f9\u0003\u001a\r\u0000\u00f9\u00fa\u0005\f\u0000"+
		"\u0000\u00fa\u0127\u0001\u0000\u0000\u0000\u00fb\u00fc\u00055\u0000\u0000"+
		"\u00fc\u00fd\u0005\u000b\u0000\u0000\u00fd\u00fe\u0003\u001a\r\u0000\u00fe"+
		"\u00ff\u0005\u0010\u0000\u0000\u00ff\u0100\u0003\u001a\r\u0000\u0100\u0101"+
		"\u0005\f\u0000\u0000\u0101\u0127\u0001\u0000\u0000\u0000\u0102\u0103\u0005"+
		"6\u0000\u0000\u0103\u0104\u0005\u000b\u0000\u0000\u0104\u0105\u0003\u001a"+
		"\r\u0000\u0105\u0106\u0005\f\u0000\u0000\u0106\u0127\u0001\u0000\u0000"+
		"\u0000\u0107\u0108\u00057\u0000\u0000\u0108\u0109\u0005\u000b\u0000\u0000"+
		"\u0109\u010a\u0003\u001a\r\u0000\u010a\u010b\u0005\u0010\u0000\u0000\u010b"+
		"\u0110\u0003\u001a\r\u0000\u010c\u010d\u0005\u0010\u0000\u0000\u010d\u010f"+
		"\u0003\u001a\r\u0000\u010e\u010c\u0001\u0000\u0000\u0000\u010f\u0112\u0001"+
		"\u0000\u0000\u0000\u0110\u010e\u0001\u0000\u0000\u0000\u0110\u0111\u0001"+
		"\u0000\u0000\u0000\u0111\u0113\u0001\u0000\u0000\u0000\u0112\u0110\u0001"+
		"\u0000\u0000\u0000\u0113\u0114\u0005\f\u0000\u0000\u0114\u0127\u0001\u0000"+
		"\u0000\u0000\u0115\u0127\u0005;\u0000\u0000\u0116\u0127\u0005<\u0000\u0000"+
		"\u0117\u0127\u0005=\u0000\u0000\u0118\u0127\u0005:\u0000\u0000\u0119\u0127"+
		"\u0005>\u0000\u0000\u011a\u011b\u00058\u0000\u0000\u011b\u011c\u0005@"+
		"\u0000\u0000\u011c\u011e\u0005\u000b\u0000\u0000\u011d\u011f\u0003\u001e"+
		"\u000f\u0000\u011e\u011d\u0001\u0000\u0000\u0000\u011e\u011f\u0001\u0000"+
		"\u0000\u0000\u011f\u0120\u0001\u0000\u0000\u0000\u0120\u0127\u0005\f\u0000"+
		"\u0000\u0121\u0127\u0005@\u0000\u0000\u0122\u0123\u0005\u000b\u0000\u0000"+
		"\u0123\u0124\u0003\u001a\r\u0000\u0124\u0125\u0005\f\u0000\u0000\u0125"+
		"\u0127\u0001\u0000\u0000\u0000\u0126\u00e2\u0001\u0000\u0000\u0000\u0126"+
		"\u00e5\u0001\u0000\u0000\u0000\u0126\u00e7\u0001\u0000\u0000\u0000\u0126"+
		"\u00f4\u0001\u0000\u0000\u0000\u0126\u00fb\u0001\u0000\u0000\u0000\u0126"+
		"\u0102\u0001\u0000\u0000\u0000\u0126\u0107\u0001\u0000\u0000\u0000\u0126"+
		"\u0115\u0001\u0000\u0000\u0000\u0126\u0116\u0001\u0000\u0000\u0000\u0126"+
		"\u0117\u0001\u0000\u0000\u0000\u0126\u0118\u0001\u0000\u0000\u0000\u0126"+
		"\u0119\u0001\u0000\u0000\u0000\u0126\u011a\u0001\u0000\u0000\u0000\u0126"+
		"\u0121\u0001\u0000\u0000\u0000\u0126\u0122\u0001\u0000\u0000\u0000\u0127"+
		"\u0153\u0001\u0000\u0000\u0000\u0128\u0129\n\u0017\u0000\u0000\u0129\u012a"+
		"\u0007\u0001\u0000\u0000\u012a\u0152\u0003\u001a\r\u0018\u012b\u012c\n"+
		"\u0016\u0000\u0000\u012c\u012d\u0007\u0002\u0000\u0000\u012d\u0152\u0003"+
		"\u001a\r\u0017\u012e\u012f\n\u0015\u0000\u0000\u012f\u0130\u0007\u0003"+
		"\u0000\u0000\u0130\u0152\u0003\u001a\r\u0016\u0131\u0132\n\u0014\u0000"+
		"\u0000\u0132\u0133\u0007\u0004\u0000\u0000\u0133\u0152\u0003\u001a\r\u0015"+
		"\u0134\u0135\n\u0013\u0000\u0000\u0135\u0136\u0007\u0005\u0000\u0000\u0136"+
		"\u0152\u0003\u001a\r\u0014\u0137\u0138\n\u0012\u0000\u0000\u0138\u0139"+
		"\u0007\u0006\u0000\u0000\u0139\u0152\u0003\u001a\r\u0013\u013a\u013b\n"+
		"\u0011\u0000\u0000\u013b\u013c\u0007\u0007\u0000\u0000\u013c\u0152\u0003"+
		"\u001a\r\u0012\u013d\u013f\n\u0018\u0000\u0000\u013e\u0140\u0003\u001c"+
		"\u000e\u0000\u013f\u013e\u0001\u0000\u0000\u0000\u0140\u0141\u0001\u0000"+
		"\u0000\u0000\u0141\u013f\u0001\u0000\u0000\u0000\u0141\u0142\u0001\u0000"+
		"\u0000\u0000\u0142\u0152\u0001\u0000\u0000\u0000\u0143\u0144\n\u0010\u0000"+
		"\u0000\u0144\u0152\u0007\b\u0000\u0000\u0145\u0146\n\u000e\u0000\u0000"+
		"\u0146\u0147\u00052\u0000\u0000\u0147\u0148\u0003\u001a\r\u0000\u0148"+
		"\u0149\u00053\u0000\u0000\u0149\u0152\u0001\u0000\u0000\u0000\u014a\u014b"+
		"\n\r\u0000\u0000\u014b\u014c\u00052\u0000\u0000\u014c\u014d\u0003\u001a"+
		"\r\u0000\u014d\u014e\u0005\u0002\u0000\u0000\u014e\u014f\u0003\u001a\r"+
		"\u0000\u014f\u0150\u00053\u0000\u0000\u0150\u0152\u0001\u0000\u0000\u0000"+
		"\u0151\u0128\u0001\u0000\u0000\u0000\u0151\u012b\u0001\u0000\u0000\u0000"+
		"\u0151\u012e\u0001\u0000\u0000\u0000\u0151\u0131\u0001\u0000\u0000\u0000"+
		"\u0151\u0134\u0001\u0000\u0000\u0000\u0151\u0137\u0001\u0000\u0000\u0000"+
		"\u0151\u013a\u0001\u0000\u0000\u0000\u0151\u013d\u0001\u0000\u0000\u0000"+
		"\u0151\u0143\u0001\u0000\u0000\u0000\u0151\u0145\u0001\u0000\u0000\u0000"+
		"\u0151\u014a\u0001\u0000\u0000\u0000\u0152\u0155\u0001\u0000\u0000\u0000"+
		"\u0153\u0151\u0001\u0000\u0000\u0000\u0153\u0154\u0001\u0000\u0000\u0000"+
		"\u0154\u001b\u0001\u0000\u0000\u0000\u0155\u0153\u0001\u0000\u0000\u0000"+
		"\u0156\u0158\u0005\u000b\u0000\u0000\u0157\u0159\u0003\u001e\u000f\u0000"+
		"\u0158\u0157\u0001\u0000\u0000\u0000\u0158\u0159\u0001\u0000\u0000\u0000"+
		"\u0159\u015a\u0001\u0000\u0000\u0000\u015a\u015e\u0005\f\u0000\u0000\u015b"+
		"\u015c\u00059\u0000\u0000\u015c\u015e\u0005@\u0000\u0000\u015d\u0156\u0001"+
		"\u0000\u0000\u0000\u015d\u015b\u0001\u0000\u0000\u0000\u015e\u001d\u0001"+
		"\u0000\u0000\u0000\u015f\u0164\u0003\u001a\r\u0000\u0160\u0161\u0005\u0010"+
		"\u0000\u0000\u0161\u0163\u0003\u001a\r\u0000\u0162\u0160\u0001\u0000\u0000"+
		"\u0000\u0163\u0166\u0001\u0000\u0000\u0000\u0164\u0162\u0001\u0000\u0000"+
		"\u0000\u0164\u0165\u0001\u0000\u0000\u0000\u0165\u001f\u0001\u0000\u0000"+
		"\u0000\u0166\u0164\u0001\u0000\u0000\u0000\u001f#+3<DKV]dru|\u0083\u0090"+
		"\u0095\u00a3\u00bc\u00cb\u00cd\u00d1\u00e0\u00ef\u0110\u011e\u0126\u0141"+
		"\u0151\u0153\u0158\u015d\u0164";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}