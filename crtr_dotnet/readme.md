# Tabelionet - Sistema de Cartórios Online

## Instruções

Siga os passos abaixo para trabalhar em dev com front:

``` powershell
# Para subir os containers
docker-compose -f .\docker-compose.dev.yml up --build

# Para excluí-los
docker-compose -f .\docker-compose.dev.yml down
```

## Publicação
``` powershell
docker compose up --build
```

## Angular
### Criar componente
ng generate component nome-componente
ng g c nome-componente
ng g c pasta/nome-componente

### Criar serviço
ng generate service services/nome-servico
ng g s services/nome-servico

### Rodar em desenvolvimento
npm run start

### Buildar em desenvolvimento
npm run build

### Buildar para produção
npm run prod