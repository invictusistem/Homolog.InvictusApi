using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Invictus.Core.Enumerations
{
    public class TipoTransacao : Enumeration
    {
        public static TipoTransacao Dinheiro = new(1, "Dinheiro");
        public static TipoTransacao Debito = new(2, "Débito");
        public static TipoTransacao CreditoVista = new(3, "Crédito à vista");
        public static TipoTransacao CreditoParcelado = new(4, "Crédito parcelado");
        


        public TipoTransacao() { }
        public TipoTransacao(int id, string name) : base(id, name) { }
       
    }

    public class TransacaoHandler : SqlMapper.IMemberMap//;;.ITypeMap//.ITypeHandler
    {
        public string ColumnName => throw new NotImplementedException();

        public Type MemberType => throw new NotImplementedException();

        public PropertyInfo Property => throw new NotImplementedException();

        public FieldInfo Field => throw new NotImplementedException();

        public ParameterInfo Parameter => throw new NotImplementedException();
    }
}