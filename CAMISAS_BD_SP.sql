USE bd_palaciocamisas
GO

<<<<<<< HEAD
CREATE PROCEDURE  ListarVentasConDetalles
=======
CREATE OR ALTER PROCEDURE  ListarVentasConDetalles
>>>>>>> origin/marytere
AS
BEGIN
    SELECT 
        v.id_venta,
        v.nombre_cliente,
        v.dni_cliente,
        v.tipo_pago,
        v.fecha,
        v.precio_total,
        v.estado,
        dv.id_camisa,
        dv.cantidad,
        dv.precio as precio_detalle,
        dv.estado as estado_detalle,
        c.descripcion as camisa_descripcion,
        c.color as camisa_color,
        c.talla as camisa_talla,
        c.manga as camisa_manga,
        m.descripcion as marca_nombre
    FROM VENTA v
    LEFT JOIN DETALLEVENTA dv ON v.id_venta = dv.id_venta
    LEFT JOIN CAMISA c ON dv.id_camisa = c.id_camisa
    LEFT JOIN MARCA m ON c.id_marca = m.id_marca
    ORDER BY v.fecha DESC, v.id_venta, dv.id_camisa
END
go

<<<<<<< HEAD
CREATE PROCEDURE ObtenerVentaPorIDConDetalles
=======
CREATE OR ALTER PROCEDURE ObtenerVentaPorIDConDetalles
>>>>>>> origin/marytere
    @id_venta INT
AS
BEGIN
    SELECT 
<<<<<<< HEAD
        v.id_venta,
        v.nombre_cliente,
        v.dni_cliente,
        v.tipo_pago,
        v.fecha,
        v.precio_total,
        v.estado,
        dv.id_camisa,
        dv.cantidad,
        dv.precio as precio_detalle,
        dv.estado as estado_detalle,
        c.descripcion as camisa_descripcion,
        c.color as camisa_color,
        c.talla as camisa_talla,
        c.manga as camisa_manga,
        m.descripcion as marca_nombre
=======
		v.id_venta,          
        v.nombre_cliente,     
        v.dni_cliente,       
        v.tipo_pago,         
        v.fecha,              
        v.precio_total,       
        v.estado,             
        dv.id_camisa,         
        dv.cantidad,          
        dv.precio,            
        dv.estado,            
        c.descripcion,        
        c.color,              
        c.talla,              
        c.manga,              
        m.descripcion         
>>>>>>> origin/marytere
    FROM VENTA v
    LEFT JOIN DETALLEVENTA dv ON v.id_venta = dv.id_venta
    LEFT JOIN CAMISA c ON dv.id_camisa = c.id_camisa
    LEFT JOIN MARCA m ON c.id_marca = m.id_marca
    WHERE v.id_venta = @id_venta
    ORDER BY dv.id_camisa
END
go

<<<<<<< HEAD
CREATE PROCEDURE ObtenerDetallesPorVenta
=======
CREATE OR ALTER PROCEDURE ObtenerDetallesPorVenta
>>>>>>> origin/marytere
    @id_venta INT
AS
BEGIN
    SELECT 
<<<<<<< HEAD
        dv.id_camisa,
        dv.cantidad,
        dv.precio,
        dv.estado,
        c.descripcion as camisa_descripcion,
        c.color as camisa_color,
        c.talla as camisa_talla,
        c.manga as camisa_manga,
        m.descripcion as marca_nombre
=======
        dv.id_camisa,        
        dv.cantidad,        
        dv.precio,           
        dv.estado,            
        c.descripcion,        
        c.color,              
        c.talla,              
        c.manga,              
        m.descripcion         
>>>>>>> origin/marytere
    FROM DETALLEVENTA dv
    INNER JOIN CAMISA c ON dv.id_camisa = c.id_camisa
    INNER JOIN MARCA m ON c.id_marca = m.id_marca
    WHERE dv.id_venta = @id_venta
END
go

<<<<<<< HEAD
CREATE PROCEDURE ctualizarEstadoVenta
=======
CREATE OR ALTER PROCEDURE ActualizarEstadoVenta
>>>>>>> origin/marytere
    @id_venta INT,
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE VENTA 
    SET estado = @estado
    WHERE id_venta = @id_venta
END
GO

<<<<<<< HEAD
CREATE PROCEDURE ctualizarEstadoDetalleVenta
=======
CREATE OR ALTER PROCEDURE ActualizarEstadoDetalleVenta
>>>>>>> origin/marytere
    @id_venta INT,
    @estado VARCHAR(20)
AS
BEGIN
    UPDATE DETALLEVENTA 
    SET estado = @estado
    WHERE id_venta = @id_venta
END
GO

-- SP para Marcas
<<<<<<< HEAD
CREATE PROCEDURE ListarMarcas
=======
CREATE OR ALTER PROCEDURE ListarMarcas
>>>>>>> origin/marytere
AS
BEGIN
    SELECT id_marca, descripcion, estado
    FROM MARCA
    WHERE estado = 'DISPONIBLE'
    ORDER BY descripcion
END
go

-- SP para Estantes
<<<<<<< HEAD
CREATE PROCEDURE ListarEstantes
=======
CREATE OR ALTER PROCEDURE ListarEstantes
>>>>>>> origin/marytere
AS
BEGIN
    SELECT id_estante, descripcion, estado
    FROM ESTANTE
    WHERE estado = 'DISPONIBLE'
    ORDER BY descripcion
END
GO

-- SP para Camisas
<<<<<<< HEAD
CREATE PROCEDURE ListarCamisas
=======
CREATE OR ALTER PROCEDURE ListarCamisas
>>>>>>> origin/marytere
AS
BEGIN
    SELECT 
        c.id_camisa,
        c.descripcion,
        c.id_marca,
        c.color,
        c.talla,
        c.manga,
        c.stock,
        c.precio_costo,
        c.precio_venta,
        c.id_estante,
        c.estado,
        m.descripcion as marca_nombre,
        e.descripcion as estante_descripcion
    FROM CAMISA c
    INNER JOIN MARCA m ON c.id_marca = m.id_marca
    INNER JOIN ESTANTE e ON c.id_estante = e.id_estante
    WHERE c.estado = 'DISPONIBLE'
    ORDER BY m.descripcion, c.descripcion
END
GO

<<<<<<< HEAD
CREATE PROCEDURE ObtenerCamisaPorID
=======
CREATE OR ALTER PROCEDURE ObtenerCamisaPorID
>>>>>>> origin/marytere
    @id_camisa INT
AS
BEGIN
    SELECT 
        c.id_camisa,
        c.descripcion,
        c.id_marca,
        c.color,
        c.talla,
        c.manga,
        c.stock,
        c.precio_costo,
        c.precio_venta,
        c.id_estante,
        c.estado,
        m.descripcion as marca_nombre,
        e.descripcion as estante_descripcion
    FROM CAMISA c
    INNER JOIN MARCA m ON c.id_marca = m.id_marca
    INNER JOIN ESTANTE e ON c.id_estante = e.id_estante
    WHERE c.id_camisa = @id_camisa
END
GO

<<<<<<< HEAD
CREATE PROCEDURE RegistrarCamisa
=======
CREATE OR ALTER PROCEDURE RegistrarCamisa
>>>>>>> origin/marytere
    @descripcion VARCHAR(45),
    @id_marca INT,
    @color VARCHAR(45),
    @talla VARCHAR(10),
    @manga VARCHAR(10),
    @stock INT,
    @precio_costo DECIMAL(7,2),
    @precio_venta DECIMAL(7,2),
    @id_estante INT,
    @estado VARCHAR(30)
AS
BEGIN
    INSERT INTO CAMISA (descripcion, id_marca, color, talla, manga, stock, precio_costo, precio_venta, id_estante, estado)
    VALUES (@descripcion, @id_marca, @color, @talla, @manga, @stock, @precio_costo, @precio_venta, @id_estante, @estado)
    
    SELECT SCOPE_IDENTITY()
END
GO

<<<<<<< HEAD
CREATE PROCEDURE ActualizarCamisa
=======
CREATE OR ALTER PROCEDURE ActualizarCamisa
>>>>>>> origin/marytere
    @id_camisa INT,
    @descripcion VARCHAR(45),
    @id_marca INT,
    @color VARCHAR(45),
    @talla VARCHAR(10),
    @manga VARCHAR(10),
    @stock INT,
    @precio_costo DECIMAL(7,2),
    @precio_venta DECIMAL(7,2),
    @id_estante INT,
    @estado VARCHAR(30)
AS
BEGIN
    UPDATE CAMISA 
    SET descripcion = @descripcion,
        id_marca = @id_marca,
        color = @color,
        talla = @talla,
        manga = @manga,
        stock = @stock,
        precio_costo = @precio_costo,
        precio_venta = @precio_venta,
        id_estante = @id_estante,
        estado = @estado
    WHERE id_camisa = @id_camisa
END
GO

<<<<<<< HEAD
CREATE PROCEDURE EliminarCamisa
=======
CREATE OR ALTER PROCEDURE EliminarCamisa
>>>>>>> origin/marytere
    @id_camisa INT
AS
BEGIN
    UPDATE CAMISA 
    SET estado = 'ELIMINADO'
    WHERE id_camisa = @id_camisa
END
GO

-- SP para Ventas
<<<<<<< HEAD
CREATE PROCEDURE ListarVentas
=======
CREATE OR ALTER PROCEDURE ListarVentas
>>>>>>> origin/marytere
AS
BEGIN
    SELECT 
        id_venta,
        nombre_cliente,
        dni_cliente,
        tipo_pago,
        fecha,
        precio_total,
        estado
    FROM VENTA
    WHERE estado = 'Activo'
    ORDER BY fecha DESC
END
GO

<<<<<<< HEAD
CREATE PROCEDURE ObtenerVentaPorID
=======
CREATE OR ALTER PROCEDURE ObtenerVentaPorID
>>>>>>> origin/marytere
    @id_venta INT
AS
BEGIN
    SELECT 
        v.id_venta,
        v.nombre_cliente,
        v.dni_cliente,
        v.tipo_pago,
        v.fecha,
        v.precio_total,
        v.estado
    FROM VENTA v
    WHERE v.id_venta = @id_venta
    
    -- También obtener los detalles
    SELECT 
        dv.id_venta,
        dv.id_camisa,
        dv.cantidad,
        dv.precio,
        dv.estado,
        c.descripcion as camisa_descripcion,
        c.color as camisa_color,
        c.talla as camisa_talla
    FROM DETALLEVENTA dv
    INNER JOIN CAMISA c ON dv.id_camisa = c.id_camisa
    WHERE dv.id_venta = @id_venta
END
GO

<<<<<<< HEAD
CREATE PROCEDURE RegistrarVenta
=======
CREATE OR ALTER PROCEDURE RegistrarVenta
>>>>>>> origin/marytere
    @nombre_cliente VARCHAR(100),
    @dni_cliente VARCHAR(15),
    @tipo_pago VARCHAR(40),
    @fecha DATE,
    @precio_total DECIMAL(10,2),
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO VENTA (nombre_cliente, dni_cliente, tipo_pago, fecha, precio_total, estado)
    VALUES (@nombre_cliente, @dni_cliente, @tipo_pago, @fecha, @precio_total, @estado)
    
    SELECT SCOPE_IDENTITY()
END
GO

<<<<<<< HEAD
CREATE PROCEDURE RegistrarDetalleVenta
=======
CREATE OR ALTER PROCEDURE RegistrarDetalleVenta
>>>>>>> origin/marytere
    @id_venta INT,
    @id_camisa INT,
    @cantidad INT,
    @precio DECIMAL(10,2),
    @estado VARCHAR(20)
AS
BEGIN
    INSERT INTO DETALLEVENTA (id_venta, id_camisa, cantidad, precio, estado)
    VALUES (@id_venta, @id_camisa, @cantidad, @precio, @estado)
    
    -- Actualizar stock de la camisa
    UPDATE CAMISA 
    SET stock = stock - @cantidad
    WHERE id_camisa = @id_camisa
END
GO