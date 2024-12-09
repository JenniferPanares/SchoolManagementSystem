using GoogleApi;
using GoogleApi.Entities.Maps.AerialView.GetVideo.Request;
using GoogleApi.Entities.Maps.AerialView.GetVideo.Response;
using GoogleApi.Entities.Maps.AerialView.Common.Enums;

using System.Threading.Tasks;

namespace SchoolManagementSystem.Services
{
    public class GoogleAerialViewService
    {
        private readonly string _apiKey;

        public GoogleAerialViewService(string apiKey)
        {
            _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        }

        /// <summary>
        /// Fetches the aerial view video URI for a given address.
        /// </summary>
        public async Task<string> GetAerialViewVideoUriAsync(string address)
        {
            var request = new GetVideoRequest
            {
                Key = _apiKey,
                Address = address
            };

            // Call the AerialView GetVideo API
            var response = await GoogleMaps.AerialView.GetVideo.QueryAsync(request);

            if (response.State == State.Active && response.Uris != null && response.Uris.ContainsKey("mp4High"))
            {
                return response.Uris["mp4High"].LandscapeUri; // Return the video URI
            }

            if (response.State == State.Processing)
            {
                return "The video is still being processed. Please try again later.";
            }

            throw new Exception("Aerial view video not found.");
        }
    }
}
