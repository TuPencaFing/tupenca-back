namespace tupenca_back.Controllers.Dto
{
    public class UserMessageDto
    {
        public List<int> UserIds { get; set; }

        public string topic { get; set; }

        public string message { get; set; }

    }
}
