using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TGS.Cartorio.Application.AppServices.Interfaces;
using TGS.Cartorio.Application.DTO;
using TGS.Cartorio.Domain.Entities;
using TGS.Cartorio.Domain.Interfaces.Services;

namespace TGS.Cartorio.Application.AppServices
{
    public class LogSistemaAppService : ILogSistemaAppService
    {
        private readonly ILogSistemaService _logSistemaService;
        public LogSistemaAppService(ILogSistemaService logSistemaService)
        {
            _logSistemaService = logSistemaService;
        }

        public async Task Add(CodLogSistema codLogSistema, object objConteudo, Exception ex = null)
        {
            try
            {
                if (objConteudo == null)
                    throw new Exception("Erro ao tentar gravar log: propriedade JsonConteudo não pode ser nulo!");

                string jsonConteudo = string.Empty;
                
                if (ex == null)
                    jsonConteudo = JsonConvert.SerializeObject(objConteudo,Formatting.Indented,new JsonSerializerSettings{
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                else
                {
                    var objJsonConteudoComException = new
                    {
                        Exception = new
                        {
                            ErrorMessage = ex != null ? ex.Message : null,
                            Source = ex != null ? ex.Source : null,
                            StackTrace = ex != null ? ex.StackTrace : null,
                        },
                        InnerException_1 = new
                        {
                            ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : null,
                            Source = ex.InnerException != null ? ex.InnerException.Source : null,
                            StackTrace = ex.InnerException != null ? ex.InnerException.StackTrace : null,
                        },
                        InnerException_2 = new
                        {
                            ErrorMessage = ex.InnerException != null
                                    && ex.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.Message : null,
                            Source = ex.InnerException != null
                                    && ex.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.Source : null,
                            StackTrace = ex.InnerException != null
                                    && ex.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.StackTrace : null,
                        },
                        InnerException_3 = new
                        {
                            ErrorMessage = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.Message : null,
                            Source = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.Source : null,
                            StackTrace = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.StackTrace : null,
                        },
                        InnerException_4 = new
                        {
                            ErrorMessage = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.InnerException.Message : null,
                            Source = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.InnerException.Source : null,
                            StackTrace = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.InnerException.StackTrace : null,
                        },
                        objConteudo = objConteudo
                    };

                    jsonConteudo = JsonConvert.SerializeObject(objJsonConteudoComException, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }

                var log = LogSistema.Create(codLogSistema.ToString(), jsonConteudo);
                await _logSistemaService.Add(log);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddByJob(CodLogSistema codLogSistema, object objConteudo, Exception ex = null)
        {
            try
            {
                if (objConteudo == null)
                    throw new Exception("Erro ao tentar gravar log: propriedade JsonConteudo não pode ser nulo!");

                string jsonConteudo = string.Empty;

                if (ex == null)
                    jsonConteudo = JsonConvert.SerializeObject(objConteudo, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                else
                {
                    var objJsonConteudoComException = new
                    {
                        Exception = new
                        {
                            ErrorMessage = ex != null ? ex.Message : null,
                            Source = ex != null ? ex.Source : null,
                            StackTrace = ex != null ? ex.StackTrace : null,
                        },
                        InnerException_1 = new
                        {
                            ErrorMessage = ex.InnerException != null ? ex.InnerException.Message : null,
                            Source = ex.InnerException != null ? ex.InnerException.Source : null,
                            StackTrace = ex.InnerException != null ? ex.InnerException.StackTrace : null,
                        },
                        InnerException_2 = new
                        {
                            ErrorMessage = ex.InnerException != null
                                    && ex.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.Message : null,
                            Source = ex.InnerException != null
                                    && ex.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.Source : null,
                            StackTrace = ex.InnerException != null
                                    && ex.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.StackTrace : null,
                        },
                        InnerException_3 = new
                        {
                            ErrorMessage = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.Message : null,
                            Source = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.Source : null,
                            StackTrace = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.StackTrace : null,
                        },
                        InnerException_4 = new
                        {
                            ErrorMessage = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.InnerException.Message : null,
                            Source = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.InnerException.Source : null,
                            StackTrace = ex.InnerException != null
                                    && ex.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException != null
                                    && ex.InnerException.InnerException.InnerException.InnerException != null ?
                                       ex.InnerException.InnerException.InnerException.InnerException.StackTrace : null,
                        },
                        objConteudo = objConteudo
                    };

                    jsonConteudo = JsonConvert.SerializeObject(objJsonConteudoComException, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                }

                var log = LogSistema.Create(codLogSistema.ToString(), jsonConteudo);
                _logSistemaService.AddByJob(log);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
