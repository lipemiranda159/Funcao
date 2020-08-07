namespace FI.AtividadeEntrevista
{
    public static class Constants
    {
        public const string CLIENTE_PARAMETER_NAME = "Nome";
        public const string CLIENTE_PARAMETER_SOBRENOME = "Sobrenome";
        public const string CLIENTE_PARAMETER_NACIONALIDADE = "Nacionalidade";
        public const string CLIENTE_PARAMETER_CEP = "CEP";
        public const string CLIENTE_PARAMETER_ESTADO = "Estado";
        public const string CLIENTE_PARAMETER_CIDADE = "Cidade";
        public const string CLIENTE_PARAMETER_LOGRADOURO = "Logradouro";
        public const string CLIENTE_PARAMETER_EMAIL = "Email";
        public const string CLIENTE_PARAMETER_TELEFONE = "Telefone";
        public const string CLIENTE_PARAMETER_ID = "Id";
        public const string CLIENTE_PARAMETER_CPF = "CPF";
        public const string INSERT_CLIENT_SP = "FI_SP_IncClienteV2";
        public const string SEARCH_CLIENT_SP = "FI_SP_ConsCliente";
        public const string VERIFY_CLIENT_SP = "FI_SP_VerificaCliente";
        public const string GET_CLIENT_SP = "FI_SP_PesqCliente";
        public const string CHANGE_CLIENT_SP = "FI_SP_AltCliente";
        public const string DELETE_CLIENT_SP = "FI_SP_DelCliente";
        public const string START_IN = "iniciarEm";
        public const string COUNT = "quantidade";
        public const string SORT_BY = "campoOrdenacao";
        public const string ORDER = "crescente";

        public const string BENEFICIARIO_PARAMETER_NAME = "Nome";
        public const string BENEFICIARIO_PARAMETER_CPF = "CPF";
        public const string BENEFICIARIO_PARAMETER_ID = "Id";
        public const string BENEFICIARIO_PARAMETER_ID_CLIENTE = "IdClient";
        public const string BENEFICIARIO_INSERT_SP = "FI_SP_IncBeneficiario";
        public const string BENEFICIARIO_SEARCH_SP = "FI_SP_ConsBeneficiario";
        public const string BENEFICIARIO_VERIFY_SP = "FI_SP_VerificaBeneficiario";
        public const string GET_BENEFICIARIO_SP = "FI_SP_PesqBeneficiario";
        public const string CHANGE_BENEFICIARIO_SP = "FI_SP_AltBeneficiario";
        public const string DELETE_BENEFICIARIO_SP = "FI_SP_DelBeneficiario";
    }
}
