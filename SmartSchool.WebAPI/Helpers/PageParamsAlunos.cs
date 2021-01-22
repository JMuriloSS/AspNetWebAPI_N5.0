namespace SmartSchool.WebAPI.Helpers
{
    /// <summary>
    /// Parâmetros para Paginação de Alunos
    /// </summary>
    public class PageParamsAlunos
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;

        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value;}
        }

        public string Matricula { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int? Ativo { get; set; } = null;
    }
}