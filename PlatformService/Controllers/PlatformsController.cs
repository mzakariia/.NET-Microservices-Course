using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/platforms")]
public class PlatformsController : ControllerBase
{
    public PlatformsController(IPlatformRepo repository, IMapper mapper,
    ICommandDataClient commandDataClient)
    {
        _repository = repository;
        _mapper = mapper;
        _commandDataClient = commandDataClient;

    }

    private readonly IPlatformRepo _repository;
    private readonly IMapper _mapper;

    private readonly ICommandDataClient _commandDataClient;

    [HttpGet]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
    {
        Console.WriteLine("--> Getting Platforms from PlatformService");

        var platformItems = _repository.GetAllPlatforms();
        return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
    }

    [HttpGet("{id}", Name = "GetPlatformById")]
    public ActionResult<IEnumerable<PlatformReadDto>> GetPlatformById([FromRoute] int id)
    {
        Console.WriteLine($"--> Getting Platform with Id {id} from PlatformService");

        var platformItem = _repository.GetPlatformById(id);
        if (platformItem != null)
        {
            return Ok(_mapper.Map<PlatformReadDto>(platformItem));
        }
        else return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<PlatformReadDto>>> CreatePlatform([FromBody] PlatformCreateDto platformCreateDto)
    {
        Console.WriteLine($"--> Creating Platform");
        Platform platfrom = _mapper.Map<Platform>(platformCreateDto);
        _repository.CreatePlatform(platfrom);
        _repository.SaveChanges();
        PlatformReadDto platformReadDto = _mapper.Map<PlatformReadDto>(platfrom);
        try
        {
            await _commandDataClient.SendPlatformToCommand(platformReadDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"--> Could not send synchronously to CommandService {ex.Message}");

        }
        return CreatedAtRoute(nameof(GetPlatformById), new { Id = platfrom.Id }, platformReadDto);
    }



}