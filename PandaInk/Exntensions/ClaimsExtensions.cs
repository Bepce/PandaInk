using System.Security.Claims;

namespace PandaInk.API.Exntensions
{
    public static class ClaimsExtensions
    {
        public static string? GetUsername(this ClaimsPrincipal user)
        {
            return user.Claims.SingleOrDefault(x => x.Type.Equals("https://www.youtube.com/redirect?event=video_description&redir_token=QUFFLUhqbUFvSW5hTWZOZ2p0c08wS0lCejI3YTR4RzJVUXxBQ3Jtc0tudGhqZ1lBVHYyRm4zQ2w1cnVEZHh4NXF2Y1ppYXhFOVFHS3lTT0hrU3NqRmhFRGJkc1VaTnUyRk1GV3pkcHJMX0sycG5sLVVPT2ZWR3VXUUVDMEJ0b0lHVHV5NDN3VFpPZkdZUkt2Y21Eczk2dmdlcw&q=http%3A%2F%2Fschemas.xmlsoap.org%2Fws%2F2005%2F05%2Fidentity%2Fclaims%2Fgivenname&v=wbD-XUmoeqw")).Value;
        }
    }
}
