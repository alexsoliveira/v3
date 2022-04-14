using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

namespace TGS.Cartorio.Domain.Entities
{
    public partial class LogSistema
    {
        public long IdLogSistema { get; set; }
        public string CodLogSistema { get; set; }
        public string JsonConteudo { get; set; }
        public DateTime DataOperacao { get; set; }

        public static LogSistema Create(string codLogSistema, string jsonConteudo)
        {
            try
            {
                if (!string.IsNullOrEmpty(jsonConteudo))
                    jsonConteudo = FormatarJsonConteudo(jsonConteudo);

                return new LogSistema
                {
                    CodLogSistema = codLogSistema,
                    JsonConteudo = jsonConteudo,
                    DataOperacao = DateTime.Now
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static LogSistema Create(string codLogSistema, object objConteudo, Exception ex = null)
        {
            try
            {
                string jsonConteudo = GetJsonConteudo(objConteudo, ex);

                if (!string.IsNullOrEmpty(jsonConteudo))
                    jsonConteudo = FormatarJsonConteudo(jsonConteudo);

                return new LogSistema
                {
                    CodLogSistema = codLogSistema,
                    JsonConteudo = jsonConteudo,
                    DataOperacao = DateTime.Now
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GetJsonConteudo(object objConteudo, Exception ex = null)
        {
            try
            {
                if (objConteudo == null)
                    throw new Exception("Erro ao tentar gravar log: propriedade JsonConteudo não pode ser nulo!");

                string jsonConteudo = string.Empty;

                if (ex == null)
                    jsonConteudo = JsonConvert.SerializeObject(objConteudo);
                else
                {
                    var innerExMessage = string.Empty;
                    if (ex.InnerException != null && !string.IsNullOrEmpty(ex.InnerException.Message))
                        innerExMessage = ex.InnerException.Message;

                    var objJsonConteudoComException = new
                    {
                        Exception = new
                        {
                            ExceptionMessage = ex.Message,
                            InnerExceptioninnerExMessage = innerExMessage,
                            StackTrace = ex.StackTrace,
                            Source = ex.Source
                        },
                        objConteudo = objConteudo
                    };

                    jsonConteudo = JsonConvert.SerializeObject(objJsonConteudoComException);
                }

                return jsonConteudo;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static string FormatarJsonConteudo(string jsonConteudo)
        {
            try
            {
                jsonConteudo = jsonConteudo
                                   .Replace(System.Environment.NewLine, "")
                                   .Replace(@"\n", "")
                                   .Replace(@"\r", "")
                                   .Replace(@"\", "")
                                   .Replace(@"\\", "")
                                   .Replace("\"{", "{")
                                   .Replace("}\"", "}")
                                   .ToString();

                return jsonConteudo;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
