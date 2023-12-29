namespace firstAppAsp.Services
{
    public class DevicesServices : IDevicesServices
    {
        private readonly ApplicationDbContext _context;
        public DevicesServices(ApplicationDbContext context) { _context = context; }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Devices.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }).OrderBy(c => c.Text).ToList();
        }
    }
}
