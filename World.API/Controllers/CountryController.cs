using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.API.Data;
using World.API.DTO.Country;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<CountryDto>> GetById(int id) {

            var country = await _countryRepository.Get(id);

            var countrydto = _mapper.Map<CountryDto>(country);
            if(country == null) 
            {
                return NoContent();
            }
            return Ok(countrydto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetAll() 
        {
            var countries = await _countryRepository.GetAll();

            var countriesDto = _mapper.Map<List<CountryDto>>(countries); 
            if (countries == null)
            { 
                return NoContent();
            }
            return Ok(countriesDto);
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Country>> Create([FromBody] CreateCountryDto countryDto)
        {
            var result = _countryRepository.IsRecordExists(x=>x.Name == countryDto.Name);

            if (result)
                return Conflict($"{countryDto.Name.ToLower().Trim()} already exists in table");

            var country = _mapper.Map<Country>(countryDto);

            await _countryRepository.Create(country);
            return CreatedAtAction("GetById", new {id = country.ID}, country);
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Country>> Update(int id, [FromBody] UpdateCountryDto countryDto) 
        {
            if(countryDto == null || id != countryDto.ID)
                return BadRequest();

            var country = _mapper.Map<Country>(countryDto);
            
            await _countryRepository.Update(country);
            return NoContent();
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(int id) 
        {
            if(id == 0)
                return BadRequest();

            var country = await _countryRepository.Get(id);

            if (country == null)
                return NotFound();

            await _countryRepository.Delete(country);
            return NoContent();
        }
    }
}
