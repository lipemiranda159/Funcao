using FI.AtividadeEntrevista;
using FI.AtividadeEntrevista.DML;
using System.Data;

public static class DaoClientExtension
{
    public static Cliente RowToCliente(this DataRow row)
    {
        return new Cliente
        {
            Id = row.Field<long>(Constants.PARAMETER_ID),
            CEP = row.Field<string>(Constants.PARAMETER_CEP),
            Cidade = row.Field<string>(Constants.PARAMETER_CIDADE),
            Email = row.Field<string>(Constants.PARAMETER_EMAIL),
            Estado = row.Field<string>(Constants.PARAMETER_ESTADO),
            Logradouro = row.Field<string>(Constants.PARAMETER_LOGRADOURO),
            Nacionalidade = row.Field<string>(Constants.PARAMETER_NACIONALIDADE),
            Nome = row.Field<string>(Constants.PARAMETER_NAME),
            Sobrenome = row.Field<string>(Constants.PARAMETER_SOBRENOME),
            Telefone = row.Field<string>(Constants.PARAMETER_TELEFONE)
        };
    }   
     
}