using Chat.Datos.Data.Repositorios.IRepositorios;
using Chat.Entidades.Modelos;
using Microsoft.AspNetCore.SignalR;

namespace Chat.SignalR
{
    public class ChatHub: Hub
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        public ChatHub(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        public async Task EnviarMensaje(int usuarioId, string IdentificadorSala, string mensaje)
        {
            var ChatRoom = _contenedorTrabajo.ChatRoom
                .GetFirstOrDefault(i => i.IdentificadorChat == IdentificadorSala);
            if (ChatRoom == null)
            {
                throw new Exception("Chat no encontrado");
            }
            var ChatMensaje = new ChatMensajes
            {
                ChatSalaId = ChatRoom.Id,
                Mensajes = mensaje,
                RemitenteID = usuarioId,
            };
            _contenedorTrabajo.ChatMensaje.Add(ChatMensaje);
            await _contenedorTrabajo.SaveAsync();
            await Clients.Group(IdentificadorSala).SendAsync("RecibirMensaje", usuarioId, mensaje);
        }


        public async Task UnirseChat(int usuarioId1, int usuarioId2)
        {
            try
            {
                string IdentificadorChat = usuarioId1 < usuarioId2 ? $"{usuarioId1}_{usuarioId2}" : $"{usuarioId2}_{usuarioId1}";

                var ChatRoom = _contenedorTrabajo.ChatRoom
                    .GetFirstOrDefault(i => i.IdentificadorChat == IdentificadorChat);
                if (ChatRoom == null)
                {
                    ChatRoom = new()
                    {
                        IdentificadorChat = IdentificadorChat,
                        UsuarioId1 = usuarioId1,
                        UsuarioId2 = usuarioId2
                    };
                    _contenedorTrabajo.ChatRoom.Add(ChatRoom);
                    await _contenedorTrabajo.SaveAsync();
                }
                await Groups.AddToGroupAsync(Context.ConnectionId, IdentificadorChat);
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en UnirseChat: {ex.Message}");
            }
           
        }

        public async Task UnirseSala(string identificadorSala)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, identificadorSala);
        }
        public async Task EnviarMensajeSala(string IdentificadorSala, int userId, string mensaje)
        {
            await Clients.Group(IdentificadorSala).SendAsync("RecibirMensaje", IdentificadorSala, userId, mensaje);
        }

    }
}
