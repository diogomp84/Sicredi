using TechTalk.SpecFlow.Assist.Attributes;

namespace AutoSicredi.Model
{
	public enum InvestorType
	{
		[TableAliases("Pessoal")]
		Pessoal,

		[TableAliases("Empresarial")]
		Empresarial
	}
}
