using Microsoft.EntityFrameworkCore.Infrastructure;

namespace projeto1.Domain
{
    public class Departamento
    {
        //3º Forma de LazyLoading
        private Action<object, string> _lazyloader { get; set; }
        private Departamento(Action<object, string> lazyloader)
        {
            _lazyloader = lazyloader;
        }


        //2º Forma de LazyLoading
        // private ILazyLoader? _lazyloader { get; set; }
        // private Departamento(ILazyLoader lazyloader)
        // {
        //     _lazyloader = lazyloader;
        // }
        public Departamento() { }
        public Departamento(string? descricao)
        {
            Descricao = descricao;
            Ativo = true;
        }

        public int Id { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }

        //2º Forma de LazyLoading
        // private List<Funcionario>? _funcionarios;
        // public List<Funcionario>? Funcionarios
        // {
        //     get => _lazyloader.Load(this, ref _funcionarios);
        //     set => _funcionarios = value;
        // }

        //3º Forma de LazyLoading
        private List<Funcionario>? _funcionarios;
        public List<Funcionario>? Funcionarios
        {
            get
            {
                _lazyloader?.Invoke(this, nameof(Funcionarios));
                return _funcionarios;
            }
            set => _funcionarios = value;
        }
    }
}