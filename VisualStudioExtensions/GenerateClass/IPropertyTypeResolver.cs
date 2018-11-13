namespace VisualStudioExtensions.GenerateClass
{
    public interface IPropertyTypeResolver
    {
        NameType Resolve(string input);
    }
}