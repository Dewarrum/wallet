create table users (
    id uuid primary key,
    name text not null,
    email text not null,
    created_at timestamp not null
);

create unique index on users (email);

create table profiles (
    id uuid primary key,
    user_id uuid references users(id),
    currency text not null,
    created_at timestamp not null
);

create table categories (
    id uuid primary key,
    user_id uuid references users(id),
    name text not null,
    description text not null,
    created_at timestamp not null
);

create table transactions (
    id uuid primary key,
    profile_id uuid references profiles(id),
    amount decimal not null,
    created_at timestamp not null,
    description text not null,
    category_id uuid references categories(id),
    type int not null,
    is_recurring boolean not null,
    payment_method int not null,
    merchant text null
);