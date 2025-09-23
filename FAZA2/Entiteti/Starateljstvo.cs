namespace Deciji_Letnji_Program.Entiteti
{
    public class Starateljstvo
    {
        public virtual int Id { get; protected set; }
        public virtual Roditelj Roditelj { get; set; }  // FK ka Roditelju
        public virtual Dete Dete { get; set; }          // FK ka Dete

        public Starateljstvo()
        {

        }
    }
}
