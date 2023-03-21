using System;

namespace ProjetoAPI.Middleware.Exceptions
{
    public class NullEx : Exception
    {
        private const string mensagemPadrao = "Não foi possível atender a solicitação. Um ou mais dados da requisição não foram encontrados.";

        public NullEx()
            : base(mensagemPadrao)
        { }

        public NullEx(string mensagem)
            : base(mensagem)
        { }

        public NullEx(Exception innerException)
            : base(mensagemPadrao, innerException)
        { }
    }
}
