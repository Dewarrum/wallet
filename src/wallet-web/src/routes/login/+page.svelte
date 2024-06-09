<script lang="ts">
	import { getAuth } from '$lib/auth/queries';
	import { ProgressBar } from '@skeletonlabs/skeleton';

	const authQuery = getAuth();

	function login() {
		window.location.href = '/api/auth/login';
	}
</script>

<h2 class="h2">Login page</h2>

{#if $authQuery.isLoading}
	<ProgressBar />
{:else if $authQuery.data}
	<p>{$authQuery.data.name}</p>
{:else}
	<button type="button" class="variant-filled-primary btn" on:click={() => login()}
		>Login via Google</button
	>
{/if}
