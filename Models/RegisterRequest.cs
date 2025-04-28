namespace eduMATE_back.Models
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Source { get; set; } = ""; //si se logea desde la web sera profesor, si es desde la app sera estudiante. 
        //por defecto dejo source vacio para despues hacer la carga del parametro que necesite (Source: web / app).
    }
}
