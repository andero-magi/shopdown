using Shop.Core.Domain;

namespace Shop.Models
{
    public class ImageViewModel
    {
        private FileToApi Image;

        public ImageViewModel(FileToApi image)
        {
            this.Image = image;
        }
    }
}
