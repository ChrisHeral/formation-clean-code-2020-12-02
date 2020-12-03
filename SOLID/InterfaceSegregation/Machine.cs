namespace SOLID.InterfaceSegregation
{
    public interface ICanPrint {

        void Print();
    }
    public interface ICanFax
    {
        void Fax();
    }
    public interface ICanScan
    {
        void Scan();

    }
    public interface ICanPhotocopy
    {
        void Photocopy();
    }
}
