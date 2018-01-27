using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API;

namespace API.Business
{
    // Classe que representa o cadastro dos usuarios
    public partial class UsuarioCadastro
    {
        public string nome;
        public string email;
        public string senha;
    }

    // Classe que representa o Login do usuario
    public partial class Login 
    {
        public string email;
        public string senha;
    }

    // Classe de execução das regras de negócio dos usuarios
    public class UsuariosBusiness
    {
       public bool Login(Login login)
        {
            try
            {
                MyDbContext contexto = new MyDbContext();
                double id = (from Usuarios x in contexto.Usuarios.ToList()
                                    where x.DesEmail == login.email && x.DesSenha == login.senha
                                    select x.IdUsuario).FirstOrDefault();
                return id > 0  ? true : false;                
            }
            catch (System.Exception)
            {
                return false;
            }

        }

        public List<string> BuscarUsuarios()
        {
            MyDbContext contexto = new MyDbContext();
            List<Usuarios> usuarios = new List<Usuarios>();

            usuarios = contexto.Usuarios.ToList();
            return (from x in usuarios select x.DesUsuario).ToList();
        }

        public string BuscarUsuarios(int id)
        {
            MyDbContext contexto = new MyDbContext();
            Usuarios usuario = contexto.Usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();
            return usuario.DesUsuario;
        }
        public bool Deletar(int id)
        {
            MyDbContext contexto = new MyDbContext();
            Usuarios usuario = contexto.Usuarios.Where(x => x.IdUsuario == id).FirstOrDefault();

            contexto.

        }
        public void Cadastrar(UsuarioCadastro usuarioCadastro)
        {
            try
            {
                ValidarUsuarioCadastro(usuarioCadastro);
                Usuarios usuario = new Usuarios();
                usuario.DesUsuario = usuarioCadastro.nome;
                usuario.DatRegistro = DateTime.Now;
                usuario.DatAlteracao = DateTime.Now;
                usuario.DesSenha = usuarioCadastro.senha;
                usuario.DesEmail = usuarioCadastro.email;

                MyDbContext contexto = new MyDbContext();
                contexto.Add(usuario);
                contexto.SaveChanges();

            }
            catch (System.Exception ex)
            {
             throw ex;
            }
        }

        public void ValidarUsuarioCadastro(UsuarioCadastro usuarioCadastro)
        {
            if (String.IsNullOrEmpty(usuarioCadastro.nome)) throw new Exception("Nome de usuário errado");         
            if (String.IsNullOrEmpty(usuarioCadastro.email)) throw new Exception("Email do usuario não informado");
            if (String.IsNullOrEmpty(usuarioCadastro.senha)) throw new Exception("Senha do usuario não informada");
        }
 

    }
}
    
