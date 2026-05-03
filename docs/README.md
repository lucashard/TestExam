# Diagramas del dominio

## Archivo editable

El diagrama de clases del dominio está en:

- `docs/domain-model.puml`

Ese archivo se puede editar como texto y visualizar como diagrama usando PlantUML.

## Flujo recomendado

1. Editar `docs/domain-model.puml`
2. Visualizar el resultado como diagrama
3. Reflejar esos cambios en las clases de `TestExam/Domain`

## Importante

En este proyecto, el diagrama **no genera clases automáticamente**.

La opción más segura es usar el diagrama como fuente visual/versionable y luego aplicar los cambios al código. Si quieres, puedo hacer esa sincronización por ti cada vez que cambies el diagrama.

## Qué representa hoy

- `Factura`
- `Cliente`
- `Direccion`
- `MateriaPrima`
- `Etapas`
- `Operarios`
- `IMateriaPrimaRepository`

## Siguiente paso posible

Si quieres edición de tipo arrastrar y soltar, también puedo dejarte una versión `drawio`, pero esa opción es peor para versionado y no ofrece generación confiable de clases C#.

