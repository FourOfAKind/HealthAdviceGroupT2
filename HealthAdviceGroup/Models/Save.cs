using System.ComponentModel.DataAnnotations.Schema;

namespace HealthAdviceGroup.Models
{
    // Model representing Advice entries saved by a User
    public class Save
    {
        // Primary Id used to identify any advice saved by a user
        public int Id { get; set; }
        // The id of the advice entry that the user has saved
        public int AdviceId{ get; set; }
        // The id of the user that saved the advice
        public string UserId{ get; set; }
        // The date that the advice was saved
        public string Date {  get; set; }
    }
}
