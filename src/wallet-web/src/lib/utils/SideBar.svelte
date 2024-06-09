<script lang="ts">
	import { base } from '$app/paths';
	import { AppRail, AppRailTile } from '@skeletonlabs/skeleton';
	import MajesticonsDollarCircleLine from '~icons/majesticons/dollar-circle-line';
	import MajesticonsBriefcaseLine from '~icons/majesticons/briefcase-line';
	import MajesticonsTagLine from '~icons/majesticons/tag-line';
	import { goto } from '$app/navigation';
	import { browser } from '$app/environment';

	let currentTile = 0;
	const routes: Record<number, string> = {
		0: `${base}`,
		1: `${base}/profiles`,
		2: `${base}/categories`
	};

	$: {
		if (browser) {
			goto(routes[currentTile]);
		}
	}
</script>

<AppRail>
	<AppRailTile bind:group={currentTile} name="tile-1" value={0} title="tile-1">
		<svelte:fragment slot="lead">
			<MajesticonsDollarCircleLine class="mx-auto h-6 w-6" />
		</svelte:fragment>
		<span>Transactions</span>
	</AppRailTile>
	<AppRailTile bind:group={currentTile} name="tile-2" value={1} title="tile-2">
		<svelte:fragment slot="lead">
			<MajesticonsBriefcaseLine class="mx-auto h-6 w-6" />
		</svelte:fragment>
		<span>Profiles</span>
	</AppRailTile>
	<AppRailTile bind:group={currentTile} name="tile-3" value={2} title="tile-3">
		<svelte:fragment slot="lead">
			<MajesticonsTagLine class="mx-auto h-6 w-6" />
		</svelte:fragment>
		<span>Categories</span>
	</AppRailTile>
</AppRail>
