using System.ComponentModel.DataAnnotations;

namespace UrlBitlyClone.Models.Home
{
    /// <summary>
    /// The home index view model.
    /// </summary>
    public class HomeIndexModel
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        [Required(ErrorMessage = "You need to enter a URL")]
        [Url(ErrorMessage = "This needs to be a correctly formed URL")]
        public string Url { get; set; }
    }
}
