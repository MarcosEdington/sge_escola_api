// PARA DESENVOLVIMENTO 

//var builder = WebApplication.CreateBuilder(args);

//// Adiciona controllers
//builder.Services.AddControllers();


//// Adiciona Swagger para documentação e teste da API
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//// Habilita CORS para permitir o frontend React (localhost:3000 é o padrão do create-react-app)
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowReactApp",
//        policy =>
//        {
//            policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002")
//                  .AllowAnyHeader()
//                  .AllowAnyMethod();
//        });
//});

//var app = builder.Build();

//// Configura o pipeline HTTP
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// Ativa o CORS
//app.UseCors("AllowReactApp");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();


//PARA PARA PRODUCAO NO RENDER

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURAÇÃO DE CORS PARA PRODUÇÃO ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("LiberarReact", policy =>
    {
        // AllowAnyOrigin permite que seu site no Netlify acesse a API
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// ------------------------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Removemos o "if IsDevelopment" do Swagger caso você queira testar a API 
// direto pelo navegador no Render, mas você pode manter se preferir.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// --- ATIVAÇÃO DO CORS ---
app.UseCors("LiberarReact");
// ------------------------

app.UseAuthorization();
app.MapControllers();

app.Run();
