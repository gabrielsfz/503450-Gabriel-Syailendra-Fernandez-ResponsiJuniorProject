# 503450-Gabriel-Syailendra-Fernandez-ResponsiJuniorProject


## DDL :
-- This script was generated by the ERD tool in pgAdmin 4.
-- Please log an issue at https://github.com/pgadmin-org/pgadmin4/issues/new/choose if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE IF NOT EXISTS public.departemen
(
    id_dep integer NOT NULL,
    nama_dep character varying(30) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT departemen_pkey PRIMARY KEY (id_dep)
);

CREATE TABLE IF NOT EXISTS public.karyawan
(
    id_karyawan integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    nama character varying(30) COLLATE pg_catalog."default" NOT NULL,
    id_dep integer NOT NULL,
    CONSTRAINT karyawan_pkey PRIMARY KEY (id_karyawan)
);

ALTER TABLE IF EXISTS public.karyawan
    ADD CONSTRAINT karyawan_id_dep_fkey FOREIGN KEY (id_dep)
    REFERENCES public.departemen (id_dep) MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

END;
