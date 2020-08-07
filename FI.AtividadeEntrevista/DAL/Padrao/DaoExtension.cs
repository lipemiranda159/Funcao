using FI.AtividadeEntrevista;
using FI.AtividadeEntrevista.DML;
using System.Data;

public static class DaoExtension
{
    public static Cliente RowToCliente(this DataRow row)
    {
        return new Cliente
        {
            Id = row.Field<long>(Constants.CLIENTE_PARAMETER_ID),
            CEP = row.Field<string>(Constants.CLIENTE_PARAMETER_CEP),
            Cidade = row.Field<string>(Constants.CLIENTE_PARAMETER_CIDADE),
            Email = row.Field<string>(Constants.CLIENTE_PARAMETER_EMAIL),
            Estado = row.Field<string>(Constants.CLIENTE_PARAMETER_ESTADO),
            Logradouro = row.Field<string>(Constants.CLIENTE_PARAMETER_LOGRADOURO),
            Nacionalidade = row.Field<string>(Constants.CLIENTE_PARAMETER_NACIONALIDADE),
            Nome = row.Field<string>(Constants.CLIENTE_PARAMETER_NAME),
            Sobrenome = row.Field<string>(Constants.CLIENTE_PARAMETER_SOBRENOME),
            Telefone = row.Field<string>(Constants.CLIENTE_PARAMETER_TELEFONE),
            CPF = row.Field<string>(Constants.CLIENTE_PARAMETER_CPF)
        };
    }

    public static Beneficiario RowToBeneficiario(this DataRow row)
    {
        return new Beneficiario
        {
            Id = row.Field<long>(Constants.BENEFICIARIO_PARAMETER_ID),
            Nome = row.Field<string>(Constants.BENEFICIARIO_PARAMETER_NAME),
            CPF = row.Field<string>(Constants.BENEFICIARIO_PARAMETER_CPF),
            IdCliente = row.Field<long>(Constants.BENEFICIARIO_PARAMETER_ID_CLIENTE),
        };
    }

}