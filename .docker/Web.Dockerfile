FROM node:20 AS base
 
FROM base AS deps
 
RUN corepack enable
WORKDIR /app
COPY ./src/wallet-web/package.json ./src/wallet-web/pnpm-lock.yaml ./
RUN --mount=type=cache,id=pnpm,target=/root/.local/share/pnpm/store pnpm fetch --frozen-lockfile
RUN --mount=type=cache,id=pnpm,target=/root/.local/share/pnpm/store pnpm install --frozen-lockfile --prod
 
FROM base AS build
 
RUN corepack enable
WORKDIR /app
COPY ./src/wallet-web/package.json ./src/wallet-web/pnpm-lock.yaml ./
RUN --mount=type=cache,id=pnpm,target=/root/.local/share/pnpm/store pnpm fetch --frozen-lockfile
RUN --mount=type=cache,id=pnpm,target=/root/.local/share/pnpm/store pnpm install --frozen-lockfile
COPY ./src/wallet-web .
RUN pnpm build
 
FROM base
 
WORKDIR /app
COPY --from=deps /app/node_modules /app/node_modules
COPY --from=build /app/build /app/build
ENV NODE_ENV production
CMD ["node", "./build/index.js", "--input-type=module"]