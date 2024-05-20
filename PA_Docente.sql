use BDAcademico
go

-- Procedimientos almacenados para TDocente
if OBJECT_ID('spListarDocente') is not null
	drop proc spListarDocente
go
create proc spListarDocente
as
begin
	select * from TDocente
end
go

exec spListarDocente
go

-- Procedimiento almacenado para agregar un docente
if OBJECT_ID('spAgregarDocente') is not null
    drop proc spAgregarDocente
go
create proc spAgregarDocente
@CodDocente char(5),
@APaterno varchar(50),
@AMaterno varchar(50),
@Nombres varchar(50),
@CodUsuario varchar(50),
@Contrasena varchar(50)
as
begin
    -- Verificar si el CodDocente ya existe en la tabla TDocente
    if not exists(select CodDocente from TDocente where CodDocente = @CodDocente)
    begin
        -- Verificar si el CodUsuario ya existe en la tabla TUsuario
        if not exists(select CodUsuario from TUsuario where CodUsuario = @CodUsuario)
        begin
            begin tran tranAgregar 
            begin try
                -- Insertar en TUsuario
                insert into TUsuario (CodUsuario, Contrasena)
                values (@CodUsuario, ENCRYPTBYPASSPHRASE('miFraseDeContraseña', @Contrasena))

                -- Insertar en TDocente
                insert into TDocente (CodDocente, APaterno, AMaterno, Nombres, CodUsuario)
                values (@CodDocente, @APaterno, @AMaterno, @Nombres, @CodUsuario)

                commit tran tranAgregar
                select CodError = 0, Mensaje = 'Docente insertado correctamente'
            end try
            begin catch
                rollback tran tranAgregar
                select CodError = 1, Mensaje = 'Error: No se ejecutó la transacción'
            end catch
        end
        else 
            select CodError = 1, Mensaje = 'Error: Usuario ya asignado en TUsuario'
    end
    else 
        select CodError = 1, Mensaje = 'Error: Código de Docente duplicado en TDocente'
end
go

exec spAgregarDocente 'D09', 'Perez', 'Gonzalez', 'Carlos', 'cpgonzalez@gmail.com', '1234'
go

-- Procedimiento almacenado para eliminar un docente
if OBJECT_ID('spEliminarDocente') is not null
    drop proc spEliminarDocente
go
create proc spEliminarDocente
@CodDocente char(5)
as
begin
    -- Verificar que el CodDocente existe en la tabla TDocente
    if exists (select CodDocente from TDocente where CodDocente = @CodDocente)
    begin
        declare @CodUsuario varchar(50)
        set @CodUsuario = (select CodUsuario from TDocente where CodDocente = @CodDocente)
        
        if exists (select CodUsuario from TUsuario where CodUsuario = @CodUsuario)
        begin
            begin tran tranEliminar
            begin try
                -- Eliminar de TDocente
                delete from TDocente where CodDocente = @CodDocente
                
                -- Eliminar de TUsuario
                delete from TUsuario where CodUsuario = @CodUsuario
                
                commit tran tranEliminar
                select CodError = 0, Mensaje = 'Docente eliminado correctamente'
            end try
            begin catch
                rollback tran tranEliminar
                select CodError = 1, Mensaje = 'Error: No se ejecutó la transacción'
            end catch
        end
        else 
            select CodError = 1, Mensaje = 'Error: No existe CodUsuario en TUsuario'
    end
    else 
        select CodError = 1, Mensaje = 'Error: CodDocente no existe en TDocente'
end
go

exec spEliminarDocente 'D010'
go

-- Login de usuario
if OBJECT_ID('spLogin') is not null
	drop proc spLogin
go
create proc spLogin
@CodUsuario varchar(50),@Contrasena varchar(50)
as
begin
	if exists (select CodUsuario from TUsuario where CodUsuario = @CodUsuario and CONVERT(varchar(50),DECRYPTBYPASSPHRASE('miFraseDeContraseña', Contrasena))=@Contrasena)
	begin
		if exists (select CodUsuario from TDocente where CodUsuario = @CodUsuario)
			select CodError = 0, Mensaje = 'Docente'
		else if exists (select CodUsuario from TAlumno where CodUsuario = @CodUsuario)
			select CodError = 0, Mensaje = 'Alumno'
		else 
			select CodError = 1, Mensaje = 'Error: Usuario no tiene privilegio de docente ni alumno, consulte al administrador'
	end
	else 
		select CodError = 1, Mensaje = 'Error: Usuario y / o contraseñas incorrectas'
end
go

exec spLogin 'cuellar@hotmail.com','1234'
go

-- Procedimiento almacenado para actualizar un alumno
if OBJECT_ID('spActualizarDocente') is not null
    drop proc spActualizarDocente
go
create proc spActualizarDocente
@CodDocente char(5),
@APaterno varchar(50),
@AMaterno varchar(50),
@Nombres varchar(50)
as
begin
    -- Verificar que el CodDocente existe en la tabla TDocente
    if exists (select CodDocente from TDocente where CodDocente = @CodDocente)
    begin
        update TDocente
        set APaterno = @APaterno,
            AMaterno = @AMaterno,
            Nombres = @Nombres
        where CodDocente = @CodDocente
        select CodError = 0, Mensaje = 'Docente actualizado correctamente'
    end
    else
        select CodError = 1, Mensaje = 'Error: CodDocente no existe en TDocente'
end
go

-- Ejemplo de ejecución del procedimiento de actualización
exec spActualizarDocente 'D010', 'Gomez', 'Perez', 'Maria'
go

if OBJECT_ID('spBuscarDocente') is not null
    drop proc spBuscarDocente
go
create proc spBuscarDocente
@CodDocente char(5)
as
begin
    if exists (select CodDocente from TDocente where CodDocente = @CodDocente)
    begin
        select * from TDocente where CodDocente = @CodDocente
    end
    else 
        select CodError = 1, Mensaje = 'Error: CodDocente no existe en la TDocente'
end
go