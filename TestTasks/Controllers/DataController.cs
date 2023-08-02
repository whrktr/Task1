using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestTasks.Model;

namespace TestTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : Controller
    {
        private readonly ApplicationContext _appContext;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DataController(ApplicationContext appContext, 
            ILogger logger,
            IMapper mapper)
        {
            _appContext = appContext;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавление записей
        /// </summary>
        /// <param name="data">Массив из пар ключ:значение</param>
        /// <returns>Результат добавления</returns>
        [HttpPost]
        public IActionResult Post(List<Dictionary<string, string>> data)
        {
            _logger.Log(LogLevel.Information, $"[POST Data] Received data: {JsonConvert.SerializeObject(data)}");

            try
            {
                _appContext.AddFromDictList(data);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }            
        }

        /// <summary>
        /// Получение списка данных из бд
        /// </summary>
        /// <param name="code">Поле код</param>
        /// <param name="value">Значение</param>
        /// <param name="id">Id в бд</param>
        /// <returns>Список данных</returns>
        [HttpGet]
        public IEnumerable<DataView> Get(int? code = null, string? value = null, int? id = null)
        {
            _logger.Log(LogLevel.Information, $"[GET Data] Received get request. Filters: code = {code}, value = {value}, id = {id}");

            var data = _appContext.Get(code, value, id);

            var result = data.Select(x => _mapper.Map<DataView>(x));
            return result;
        }
    }
}
