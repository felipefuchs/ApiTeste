-- Script: create_table_usuarios.sql
-- Cria a tabela de usuários para PostgreSQL

CREATE TABLE IF NOT EXISTS public.usuarios (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    senha VARCHAR(200) NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT now()
);

-- Índice único opcional para evitar nomes duplicados (descomente se desejar)
-- CREATE UNIQUE INDEX IF NOT EXISTS ux_usuarios_nome ON public.usuarios (lower(nome));

-- Exemplo de inserção:
-- INSERT INTO public.usuarios (nome, senha) VALUES ('joao', 'hash_da_senha');
