namespace Suftnet.Cos.DataAccess
{
   using System;

   public class OptionDto
    {
       public int Id { get; set; }
       public string Title { get; set; }

        public string Value { get; set; }
        public override bool Equals(object obj)
        {
            OptionDto obj2 = obj as OptionDto;
            if (obj2 == null) return false;
            return Title == obj2.Title;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
