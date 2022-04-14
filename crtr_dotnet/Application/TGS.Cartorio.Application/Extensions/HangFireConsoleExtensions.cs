using Hangfire.Console;
using Hangfire.Server;
using Newtonsoft.Json;
using System;
using TGS.Cartorio.Application.DTO;

namespace TGS.Cartorio.Application.Extensions
{
    public static class HangFireConsoleExtensions
    {
        public static void CreateExceptionMessage(this PerformContext context, Exception ex)
        {
            try
            {
                RecordMessage(context, null, ex, isError: true);
            }
            catch (Exception) { }
        }


        public static void CreateExceptionMessage(this PerformContext context, Exception ex, CodLogSistema codLogSistema)
        {
            try
            {
                RecordMessage(context, codLogSistema, ex, isError: true);
            }
            catch (Exception) { }
        }

        public static void CreateExceptionMessage(this PerformContext context, Exception ex, CodLogSistema codLogSistema, string msg)
        {
            try
            {
                RecordMessage(context, codLogSistema, ex, msg, isError: true);
            }
            catch (Exception) { }
        }

        public static void CreateConsoleMessage(this PerformContext context, CodLogSistema? codLogSistema, string msg)
        {
            try
            {
                RecordMessage(context, codLogSistema, msg: msg, isError: false);
            }
            catch (Exception) { }
        }


        private static void RecordMessage(PerformContext context, CodLogSistema? codLogSistema, Exception ex = null, string msg = null, bool isError = false)
        {
            try
            {
                context.WriteLine();
                context.WriteLine("-----------------------------");
                context.WriteLine();

                if (codLogSistema.HasValue)
                    context.WriteLine($"CodLogSistema {codLogSistema.Value}");

                if (isError && !string.IsNullOrEmpty(msg))
                {
                    context.WriteLine($"Ocorreu o seguinte erro:");
                    context.WriteLine();
                    context.WriteLine(msg);
                    context.WriteLine(GetExceptionsText(ex));
                }
                else if (isError)
                {
                    context.WriteLine($"Ocorreu o seguinte erro:");
                    context.WriteLine();
                    context.WriteLine(GetExceptionsText(ex));
                }
                else if (!string.IsNullOrEmpty(msg))
                {
                    context.WriteLine();
                    context.WriteLine(msg);
                }
            }
            catch (Exception) { }
        }

        private static string GetExceptionsText(Exception ex)
        {
            try
            {
                var objError = new
                {
                    Exception = new
                    {
                        ErrorMessage = ex.Message,
                        Source = ex.Source,
                        StackTrace = ex.StackTrace,
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
                    }
                };

                var objTextError = JsonConvert.SerializeObject(objError)
                                              .Replace(System.Environment.NewLine, "")
                                              .Replace(@"\n", "")
                                              .Replace(@"\r", "")
                                              .Replace(@"\", "")
                                              .Replace(@"\\", "")
                                              .Replace("\"{", "{")
                                              .Replace("}\"", "}");

                return objTextError.Replace(System.Environment.NewLine, "")
                                   .Replace(@"\n", "")
                                   .Replace(@"\r", "")
                                   .Replace(@"\", "")
                                   .Replace(@"\\", "")
                                   .Replace("\"{", "{")
                                   .Replace("}\"", "}");
            }
            catch (Exception) 
            {
                return ex.Message;
            }
        }
    }
}
