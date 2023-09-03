using RestaurantStore.Core.Validation;

namespace RestaurantStore.Core.Dtos
{
    public class DeleteUserDto
    {
        [SafeText]
        public string Id { get; set; }
        public bool isDelete { get; set; }
    }
}
