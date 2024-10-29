using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnayTalepUygulamasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [HttpGet("get-departments")]
        public IActionResult GetDepartments()
        {
            var departments = new List<string>
            {
                "Finans ve Kontrol (FI)",
                "Satış ve Dağıtım (SD)",
                "Malzeme Yönetimi (MM)",
                "Üretim Planlama (PP)",
                "Kalite Yönetimi (QM)",
                "İnsan Kaynakları (HR)",
                "Müşteri İlişkileri Yönetimi (CRM)",
                "Tedarik Zinciri Yönetimi (SCM)",
                "Proje Sistemi (PS)",
                "Varlık Yönetimi (PM)",
                "İş Zekası (BI)",
                "Çevre, Sağlık ve Güvenlik (EHS)",
                "Sistem Yönetimi (Basis)",
                "Teknik Geliştirme (ABAP)",
                "Veri Yönetimi (MDM)",
                "Bilgi Teknolojileri (IT)" 
            };

            return Ok(departments);
        }
    }
}
