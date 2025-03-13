using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs;
using TaskManagementApi.Interfaces;
using TaskManagementApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly IGenericRepository<Label> _labelRepository;
        private readonly IMapper _mapper;

        public LabelController(IGenericRepository<Label> labelRepository, IMapper mapper)
        {
            _labelRepository = labelRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllLabel")]
        public async Task<IActionResult> GetAllLabels()
        {
            try
            {
                var label = await _labelRepository.GetAllAsync();
                var labelResponse = _mapper.Map<List<LabelDto>>(label);
                return Ok(labelResponse);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while fetching labels." });
            }
        }

        [HttpGet("{id}", Name = "GetLabelById")]
        public async Task<IActionResult> GetLabelById(int id)
        {
            try
            {
                var label = await _labelRepository.GetByIdAsync(id);
                if (label == null)
                {
                    return NotFound(new { message = "Label not found" });
                }

                var labelResponse = _mapper.Map<LabelDto>(label);
                return Ok(labelResponse);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while fetching the label." });
            }
        }

        [Authorize]
        [HttpPost(Name = "AddLabel")]
        public async Task<IActionResult> AddLabel([FromBody] LabelDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var label = _mapper.Map<Label>(createDto);
                var createdLabel = await _labelRepository.AddAsync(label);
                var labelResponse = _mapper.Map<LabelDto>(createdLabel);

                return CreatedAtRoute("GetLabelById", new { id = createdLabel.Id }, labelResponse);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while creating the Label." });
            }
        }
    }
}
