<script lang="ts">
	import Button from '$lib/components/ui/button/button.svelte';
	import * as Card from '$lib/components/ui/card/index';
	import { signIn, signOut } from '@auth/sveltekit/client';
	import ArrowUpRight from 'lucide-svelte/icons/arrow-up-right';

	export let data;
	const { session } = data;

	const signInOptions = {
		callbackUrl: '/app'
	};
</script>

<div class="container mx-auto">
	<h1 class="scroll-m-20 text-4xl font-extrabold tracking-tight lg:mb-8 lg:mt-4 lg:text-5xl">
		Wallet App
	</h1>

	{#if session?.user}
		<Card.Root class="flex flex-col items-center lg:mx-auto lg:w-1/3 xl:col-span-2">
			<Card.Header>
				<Card.Title>You are signed in as {session.user.email}</Card.Title>
			</Card.Header>
			<Card.Content class="flex flex-col items-center">
				<Button href="/app">Continue as {session.user.email}</Button>
			</Card.Content>
			<Card.Footer class="flex flex-col items-center">
				<small class="mb-2 text-sm font-medium leading-none"
					>Sign Out if you want to use another email</small
				>
				<Button variant="secondary" size="sm" class="gap-1" on:click={() => signOut()}>
					Sign Out
					<ArrowUpRight class="h-4 w-4" />
				</Button>
			</Card.Footer>
		</Card.Root>
	{:else}
		<Card.Root class="flex flex-col items-center lg:mx-auto lg:w-1/3 xl:col-span-2">
			<Card.Header>
				<Card.Title>Login using following options</Card.Title>
			</Card.Header>
			<Card.Content class="flex flex-col items-center gap-4">
				<div class="wrapper-form">
					<Button on:click={() => signIn('google', signInOptions)}>Sign In with Google</Button>
				</div>
				<div class="wrapper-form">
					<Button on:click={() => signIn('github', signInOptions)}>Sign In with GitHub</Button>
				</div>
			</Card.Content>
		</Card.Root>
	{/if}
</div>
