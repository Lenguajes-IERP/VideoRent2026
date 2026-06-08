using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VideoRent.Business;
using VideoRent.Domain;

namespace VideoRent.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly PeliculaBusiness peliculaBusiness;

        // Constructor inyección de dependencia
        public PeliculasController(IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionStrings:VideoRentDB"];
            this.peliculaBusiness = new PeliculaBusiness(connectionString);
        }

        /// <summary>
        /// Gets all peliculas.
        /// GET: api/peliculas
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<Pelicula>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Pelicula>> GetAllPeliculas()
        {
            try
            {
                // TODO: Implement GetAllPeliculas in PeliculaBusiness
                return StatusCode(StatusCodes.Status501NotImplemented, 
                    "GetAllPeliculas method not yet implemented in PeliculaBusiness.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database.");
            }
        }

        /// <summary>
        /// Gets a specific pelicula by ID.
        /// GET: api/peliculas/{id}
        /// </summary>
        /// <param name="id">The ID of the pelicula to retrieve.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Pelicula), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Pelicula> GetPeliculaById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pelicula ID must be a positive integer.");
            }

            try
            {
                // TODO: Implement GetPeliculaById in PeliculaBusiness
                return StatusCode(StatusCodes.Status501NotImplemented, 
                    "GetPeliculaById method not yet implemented in PeliculaBusiness.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error retrieving data from the database.");
            }
        }

        /// <summary>
        /// Creates a new pelicula.
        /// POST: api/peliculas
        /// </summary>
        /// <param name="pelicula">The pelicula object to create.</param>
        [HttpPost]
        [ProducesResponseType(typeof(Pelicula), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Pelicula> AddPelicula([FromBody] Pelicula pelicula)
        {
            // TODO improve with a DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Returns detailed validation errors
            }

            // Basic null check for the pelicula object
            if (pelicula == null)
            {
                return BadRequest("Pelicula data is required.");
            }

            // Validaciones
            if (string.IsNullOrWhiteSpace(pelicula.Titulo))
            {
                return BadRequest("Pelicula's title is required.");
            }

            if (pelicula.Genero == null || pelicula.Genero.GeneroId <= 0)
            {
                return BadRequest("Pelicula must have a valid genre.");
            }

            if (pelicula.Actores == null || pelicula.Actores.Count < 1)
            {
                return BadRequest("Pelicula must have at least one actor.");
            }

            try
            {
                // The Insertar method in PeliculaBusiness will set the PeliculaId
                peliculaBusiness.Insertar(pelicula);
                // Returns 201 Created status and a link to the newly created resource
                return CreatedAtAction(nameof(GetPeliculaById), 
                    new { id = pelicula.PeliculaId }, pelicula);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error creating the pelicula in the database.");
            }
        }

        /// <summary>
        /// Updates an existing pelicula.
        /// PUT: api/peliculas/{id}
        /// </summary>
        /// <param name="id">The ID of the pelicula to update.</param>
        /// <param name="pelicula">The updated pelicula object.</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdatePelicula(int id, [FromBody] Pelicula pelicula)
        {
            if (id <= 0)
            {
                return BadRequest("Pelicula ID must be a positive integer.");
            }

            // Basic model state validation
            // TODO improve with DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Ensure the ID in the URL matches the ID in the body
            if (id != pelicula.PeliculaId)
            {
                return BadRequest("Pelicula ID in URL does not match Pelicula ID in body.");
            }

            // Basic null/empty checks
            if (pelicula == null || string.IsNullOrWhiteSpace(pelicula.Titulo))
            {
                return BadRequest("Pelicula data (ID, title) is required.");
            }

            if (pelicula.Genero == null || pelicula.Genero.GeneroId <= 0)
            {
                return BadRequest("Pelicula must have a valid genre.");
            }

            if (pelicula.Actores == null || pelicula.Actores.Count < 1)
            {
                return BadRequest("Pelicula must have at least one actor.");
            }

            try
            {
                // TODO: Implement GetPeliculaById and UpdatePelicula in PeliculaBusiness
                return StatusCode(StatusCodes.Status501NotImplemented, 
                    "UpdatePelicula method not yet implemented in PeliculaBusiness.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error updating the pelicula in the database.");
            }
        }

        /// <summary>
        /// Deletes a pelicula by ID.
        /// DELETE: api/peliculas/{id}
        /// </summary>
        /// <param name="id">The ID of the pelicula to delete.</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeletePelicula(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pelicula ID must be a positive integer.");
            }

            try
            {
                // TODO: Implement GetPeliculaById and DeletePelicula in PeliculaBusiness
                return StatusCode(StatusCodes.Status501NotImplemented, 
                    "DeletePelicula method not yet implemented in PeliculaBusiness.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    "Error deleting the pelicula from the database.");
            }
        }
    }
}
