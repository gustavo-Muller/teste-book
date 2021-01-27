using System;
using System.IO;

namespace MaximaTechCriptografia.Business
{
    public class UtilitarioLogger
    {
        private static string _pathLog;
        private const string caminhoLogErro = "/LogErro";
        public static string PathLog
        {
            get { return _pathLog; }
            set { _pathLog = value; }

        }

        public static void GraveLog(string mensagem)
        {
            if (string.IsNullOrEmpty(_pathLog))
                return;

            if (!Directory.Exists(_pathLog + caminhoLogErro))
            {
                Directory.CreateDirectory(_pathLog + caminhoLogErro);
            }

            File.AppendAllText($"{_pathLog}{caminhoLogErro}\\Erros.txt", $"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} - {mensagem}{Environment.NewLine}");
        }
    }
}
