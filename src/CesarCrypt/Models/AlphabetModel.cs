using System.Collections.Generic;
using System.Linq;

namespace CesarCrypt.Models
{
    public struct AlphabetModel
    {
        public int position { get; set; }
        public string letter { get; set; }

        private List<AlphabetModel> GetAlphabet()
        {
            List<AlphabetModel> _return = new List<AlphabetModel>();
            var Alphabet = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
            int position = 0;
            foreach (var lt in Alphabet.Split(','))
            {
                _return.Add(new AlphabetModel { letter = lt, position = position });
                position++;
            }
            return _return;
        }

        public string GetLetterByPosition(int position)
        {
            return GetAlphabet().Where(p => p.position == position).FirstOrDefault().letter;
        }

        public int GetPositionByLetter(char letter)
        {
            return GetAlphabet().Where(p => p.letter == letter.ToString()).FirstOrDefault().position;
        }
    }
}
