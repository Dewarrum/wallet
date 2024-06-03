<script lang="ts">
	import { base } from '$app/paths';
	import { userStore } from '../../stores/user';
	import { onMount } from 'svelte';

	onMount(async () => {
		$userStore = {
			...$userStore,
			isLoading: true
		};

		const response = await fetch('/api/account/my-info');
		$userStore = await response.json();
	});
</script>

<h1>Login page</h1>

{#if $userStore.isLoading}
	<p>Loading</p>
{:else if $userStore.user}
	<p>{$userStore.user.name}</p>
{:else}
	<form method="get" action="/api/account/signin">
		<input type="hidden" name="authenticationProvider" value="Google" />
		<input type="hidden" name="backUrl" value="{base}/login" />
		<button type="submit">Login via Google</button>
	</form>
{/if}
