
using server.Responses;

namespace server.Constants
{
    public class HttpClass<T>
    {

        public BaseResponse<IQueryable<T>> Get(IQueryable<T> data)
        {
            return new BaseResponse<IQueryable<T>>
            {
                Message = "Datos obtenidos con exito",
                Data = data,
                Status = 200
            };
        }

        public BaseResponse<T> GetJustOne(T data)
        {
            return new BaseResponse<T>
            {
                Message = "Dato obtenido con exito",
                Data = data,
                Status = 200
            };
        }

        public BaseResponse<T> Post(T data)
        {
            return new BaseResponse<T>
            {
                Message = "Se creo el dato",
                Data = data,
                Status = 201
            };
        }

        public BaseResponse<T> Put(T data)
        {
            return new BaseResponse<T>
            {
                Message = "Se edito el dato",
                Data = data,
                Status = 200
            };
        }
        public BaseResponse<char> Delete()
        {
            return new BaseResponse<char>
            {
                Message = "Se elimino el dato",
                Data = ' ',
                Status = 200
            };
        }

        // ============= erros =============
        public BaseResponse<char> NotfoundFunc(string? message = null)
        {
            string msg = message != null ? message : "No se encontro ningun dato";

            return new BaseResponse<char>
            {
                Message = msg,
                Data = ' ',
                Status = 404
            };
        }

        public BaseResponse<char> BadRequest(string? message = null)
        {
            string msg = message != null ? message : "Ha ocurrido un error";

            return new BaseResponse<char>
            {
                Message = msg,
                Data = ' ',
                Status = 409
            };
        }

    }



}