# API para búsqueda de vuelos
Api simple para consultar rutas basado en el servicio https://recruiting-api.newshore.es/api/flights/2

## Correr el proyecto
Está listo para ser ejecutado, si usa visual studio code, utilice el siguiente comando
```
donet run
```

## Métodos
La API cuenta con 2 endpoints de tipo GET:

Obtener los vuelos, enviando el origen y el destino
```
https://localhost:7099/flight?origin={origen}&destination={destino}
```

Obtener las tasas de conversión (USD,MXN,EUR)
```
https://localhost:7099/currency
```

_Para la tasa de conversión se utiliza la API gratuita de https://api.freecurrencyapi.com_
