namespace VisualStudioExtensions.GenerateClass
{
    public class GenerateClassInputDataProvider : IGenerateClassInputDataProvider
    {
        public GenerateClassInputData GetInputData(string selection)
        {
            var form = new GenerateClassForm();
            form.ShowDialog();

            var inputData = form.GetInputData();
            return inputData;
        }
    }
}
