using System;

namespace SOLID.InterfaceSegregation
{
    public class MultiFunctionCopier : ICanPrint, ICanFax, ICanPhotocopy, ICanScan {

        
        public void Print() {
            Console.WriteLine("Print pages");
        }

        public void Fax() {
            Console.WriteLine("Fax pages");
        }

        public void Scan() {
            Console.WriteLine("Scan pages");
        }

        public void Photocopy() {
            Console.WriteLine("Photocopy pages");
        }

    }
}
