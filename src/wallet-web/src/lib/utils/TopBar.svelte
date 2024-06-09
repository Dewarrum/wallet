<script lang="ts">
	import { base } from '$app/paths';
	import { getAuth } from '$lib/auth/queries';
	import { AppBar, ProgressBar } from '@skeletonlabs/skeleton';
	import IconMajesticonsUserLine from '~icons/majesticons/user-line';

	const authQuery = getAuth();
</script>

<AppBar gridColumns="grid-cols-3" slotDefault="place-self-center" slotTrail="place-content-end">
	<svelte:fragment slot="lead">
		<span></span>
	</svelte:fragment>

	<a href={base}><h1 class="h1">Wallet</h1></a>

	<svelte:fragment slot="trail">
		<IconMajesticonsUserLine />
		{#if $authQuery.isLoading}
			<ProgressBar />
		{:else if $authQuery.isError}
			<a href="{base}/login">Login</a>
		{:else if $authQuery.isSuccess}
			<span>{$authQuery.data.name}</span>
		{/if}
	</svelte:fragment>
</AppBar>
