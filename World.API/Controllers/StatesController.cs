using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using World.API.DTO.State;
using World.API.Models;
using World.API.Repository.IRepository;

namespace World.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _statesRepository;
        private readonly IMapper _mapper;

        public StatesController(IStatesRepository statesRepository, IMapper mapper)
        {
            _statesRepository = statesRepository;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<StatesDto>> GetById(int id)
        {

            var state = await _statesRepository.Get(id);

            var stateDto = _mapper.Map<StatesDto>(state);
            if (state == null)
            {
                return NoContent();
            }
            return Ok(stateDto);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<IEnumerable<StatesDto>>> GetAll()
        {
            var states = await _statesRepository.GetAll();

            var statesDto = _mapper.Map<List<StatesDto>>(states);
            if (states == null)
            {
                return NoContent();
            }
            return Ok(statesDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CreateStatesDto>> Create([FromBody] CreateStatesDto statesDto)
        {
            var result = _statesRepository.IsRecordExists(x => x.Name == statesDto.Name);

            if (result)
                return Conflict($"{statesDto.Name.ToLower().Trim()} already exists in table");

            var states = _mapper.Map<States>(statesDto);

            await _statesRepository.Create(states);
            return CreatedAtAction("GetById", new { id = states.Id }, states);
        }

        [HttpPut("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<States>> Update(int id, [FromBody] UpdateStatesDto statesDto)
        {
            if (statesDto == null || id != statesDto.Id)
                return BadRequest();

            var states = _mapper.Map<States>(statesDto);

            await _statesRepository.Update(states);
            return NoContent();
        }

        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteById(int id)
        {
            if (id == 0)
                return BadRequest();

            var state = await _statesRepository.Get(id);

            if (state == null)
                return NotFound();

            await _statesRepository.Delete(state);
            return NoContent();
        }
    }
}
